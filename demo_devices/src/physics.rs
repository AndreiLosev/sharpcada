use plc::task::MutProgram;
use crate::mb_context::{AppResult, MbContext};
use crate::cooling::AIR_FLOW;

pub const HEAT_IRRADIATION: f32 = 0.5;
pub const THERMAL_CAPACITY: f32 = 400.0;
pub const HEATER_POWER: f32 = 50.0;

pub struct Physics {
    q_from_passiv_cooler: f32,
} 

impl Physics {
    pub fn new() -> Self {
        Self {
            q_from_passiv_cooler: 0.0,
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
        let new_temp = temp - HEAT_IRRADIATION * (temp - air_temp) / THERMAL_CAPACITY;

        if heating {
            let new_temp = new_temp + HEATER_POWER / THERMAL_CAPACITY; 
            context.set_tank_temperature(new_temp)?;

            return Ok(());
        } 
    
        context.set_tank_temperature(new_temp)?;

        Ok(())
    }

    fn calc_q_from_passiv_coolor(
        &mut self,
        context: &mut plc::ModbusContext,
    ) -> AppResult<()> {
        if !context.get_passive_cooling_state()? {
            self.q_from_passiv_cooler = 0.0;
            return Ok(());
        }

        let outside_temp = context.get_outside_temperature()?;
        let inside_temp = context.get_inside_temperature()?;
        let q = 1.2 * AIR_FLOW * (inside_temp - outside_temp) / 3600.0;
        self.q_from_passiv_cooler = q;

        Ok(())
    }
}

impl MutProgram for Physics {
    fn run(&mut self, context: &mut plc::ModbusContext) -> AppResult<()> {
        self.tank_level_handler(context)?;
        self.tank_level_handler(context)?;

        Ok(())
    }
}
