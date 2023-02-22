use plc::task::MutProgram;
use crate::mb_context::{AppResult, MbContext};
use crate::cooling::{AIR_FLOW, COLLER_POWER};
use crate::DEBUG_LOG;

pub const HEAT_IRRADIATION: f32 = 0.5;
pub const THERMAL_CAPACITY: f32 = 400.0;
pub const ROOM_THERMAL_CAPACITY: f32 = 293.0;
pub const HEATER_POWER: f32 = 50.0;

pub struct Physics {
    q_cooler: f32,
    q_from_tank: f32,
} 

impl Physics {
    pub fn new() -> Self {
        Self {
            q_cooler: 0.0,
            q_from_tank: 0.0,
        }
    }

    fn tank_level_handler(&mut self, context: &mut plc::ModbusContext) -> AppResult<()> {
        let level = context.get_tank_lavel()?;
        if context.get_filling_valve_state()? {
            context.set_tank_lavel(level + 18)?;
        }

        if context.get_drain_valve_state()? {
            let new_level = level - 37;
            context.set_tank_lavel(new_level)?;
        }

        Ok(())
    }

    fn tank_temperature_handler(
        &mut self,
        context: &mut plc::ModbusContext,
    ) -> AppResult<()> {
        let heating = context.get_tank_heater()?;
        let temp = context.get_tank_temperature()?;
        let air_temp = context.get_inside_temperature()?;
        self.q_from_tank = HEAT_IRRADIATION * (temp - air_temp);
        let new_temp = temp - self.q_from_tank / THERMAL_CAPACITY;

        if heating {
            let new_temp = new_temp + HEATER_POWER / THERMAL_CAPACITY; 
            context.set_tank_temperature(new_temp)?;

            return Ok(());
        } 
    
        context.set_tank_temperature(new_temp)?;

        Ok(())
    }

    fn calc_q_coolor(
        &mut self,
        context: &mut plc::ModbusContext,
    ) -> AppResult<()> {
        if !context.get_passive_cooling_state()? {
            self.q_cooler = 0.0;
            return Ok(());
        }

        let supply_air = context.get_outside_temperature()?;
        let inside_temp = context.get_inside_temperature()?;
        let q = 1.2 * AIR_FLOW * (inside_temp - supply_air) / 3600.0;
        self.q_cooler = q;

        Ok(())
    }

    fn calc_supplay_air(&mut self, context: &mut plc::ModbusContext) -> AppResult<()> {
        let supply_air = context.get_supply_air_temperature()?;
        let outside_temp = context.get_outside_temperature()?;
        let active_cooling = context.get_active_cooling_state()?;
        if !active_cooling {
            let new_supply_air = if supply_air < outside_temp {
                supply_air + 0.07
            } else {
                outside_temp
            };

            context.set_supply_air_temperature(new_supply_air)?;
            return Ok(());
        }

        let cooling_temp = outside_temp - (COLLER_POWER * 3600.0 / (1.2 * AIR_FLOW));

        let new_supply_air = if supply_air > cooling_temp {
            supply_air - 0.07
        } else {
            cooling_temp
        };

        context.set_supply_air_temperature(new_supply_air)?;

        Ok(())
    }

    fn calc_inside_temperature(
        &mut self,
        context: &mut plc::ModbusContext,
    ) -> AppResult<()> {
        self.calc_supplay_air(context)?;
        self.calc_q_coolor(context)?;

        let temp = context.get_inside_temperature()?;

        let new_temp = temp + (self.q_from_tank - self.q_cooler) / ROOM_THERMAL_CAPACITY;
        context.set_inside_temperature(new_temp)?;

        Ok(())
    }

    fn log(&self, context: &mut plc::ModbusContext) -> AppResult<()> {

        if !DEBUG_LOG {
            return Ok(());
        }
    
        let t = std::time::SystemTime::now();
        let since_the_epoch = t
            .duration_since(std::time::UNIX_EPOCH)
            .expect("Time went backwards");

        if (since_the_epoch.as_millis() % 1000) < 100 {

            let in_t = context.get_inside_temperature()?;
            let tank_t = context.get_tank_temperature()?;
            let supply_t = context.get_supply_air_temperature()?;
            println!("inT: {}, tankT: {}, supplyT: {}", in_t, tank_t, supply_t);
        }
        
        Ok(())
    }

}

impl MutProgram for Physics {
    fn run(&mut self, context: &mut plc::ModbusContext) -> AppResult<()> {
        self.tank_level_handler(context)?;
        self.tank_temperature_handler(context)?;
        self.calc_inside_temperature(context)?;
        self.log(context)?;
        Ok(())
    }
}
