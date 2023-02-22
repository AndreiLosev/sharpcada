use plc::ModbusContext;
use std::error::Error;
use plc::pls_std::BitWord;

pub type  AppResult<T> = Result<T, Box<dyn Error>>;

pub trait MbContext {
    fn get_outside_temperature(&self) -> AppResult<f32>;
    fn set_outside_temperature(&mut self, value: f32) -> AppResult<()>;
    
    fn get_filling_valve_state(&self) -> AppResult<bool>;
    fn set_filling_valve_state(&mut self, state: bool) -> AppResult<()>;

    fn get_drain_valve_state(&self) -> AppResult<bool>;
    fn set_drain_valve_state(&mut self, state: bool) -> AppResult<()>;

    fn get_tank_run(&self) -> AppResult<bool>;
    fn set_tank_run(&mut self, state: bool) -> AppResult<()>;

    fn get_tank_heater(&self) -> AppResult<bool>;
    fn set_tank_heater(&mut self, state: bool) -> AppResult<()>;

    fn get_tank_lavel(&self) -> AppResult<u16>;
    fn set_tank_lavel(&mut self, value: u16) -> AppResult<()>;

    fn get_tank_temperature(&self) -> AppResult<f32>;
    fn set_tank_temperature(&mut self, value: f32) -> AppResult<()>;

    fn get_passive_cooling_state(&self) -> AppResult<bool>;
    fn set_passive_cooling_state(&mut self, state: bool) -> AppResult<()>;

    fn get_active_cooling_state(&self) -> AppResult<bool>;
    fn set_active_cooling_state(&mut self, state: bool) -> AppResult<()>;

    fn get_inside_temperature(&self) -> AppResult<f32>;
    fn set_inside_temperature(&mut self, value: f32) -> AppResult<()>;

    fn get_supply_air_temperature(&self) -> AppResult<f32>;
    fn set_supply_air_temperature(&mut self, value: f32) -> AppResult<()>;
}

impl MbContext for ModbusContext {
    fn get_outside_temperature(&self) -> AppResult<f32> {
        let result = self.get_holdings_as_f32(0)?;
        Ok(result)
    }

    fn set_outside_temperature(&mut self, value: f32) -> AppResult<()> {
        self.set_holdings_from_f32(0, value)?;
        Ok(())
    }

    fn get_filling_valve_state(&self) -> AppResult<bool> {
        let register = self.get_holding(2)?;
        let result = register.get_bit(0)?;
        Ok(result)
    }

    fn set_filling_valve_state(&mut self, state: bool) -> AppResult<()> {
        let mut register = self.get_holding(2)?;
        register.set_bit(0, state)?;
        self.set_holding(2, register)?;
        Ok(())
    }

    fn get_drain_valve_state(&self) -> AppResult<bool> {
        let register = self.get_holding(2)?;
        let result = register.get_bit(1)?;
        Ok(result)
    }

    fn set_drain_valve_state(&mut self, state: bool) -> AppResult<()> {
        let mut register = self.get_holding(2)?;
        register.set_bit(1, state)?;
        self.set_holding(2, register)?;
        Ok(())
    }

    fn get_tank_run(&self) -> AppResult<bool> {
        let register = self.get_holding(2)?;
        let result = register.get_bit(2)?;
        Ok(result)
    }

    fn set_tank_run(&mut self, state: bool) -> AppResult<()> {
        let mut register = self.get_holding(2)?;
        register.set_bit(2, state)?;
        self.set_holding(2, register)?;
        Ok(())
    }

    fn get_tank_heater(&self) -> AppResult<bool> {
        let register = self.get_holding(2)?;
        let result = register.get_bit(3)?;
        Ok(result)
    }

    fn set_tank_heater(&mut self, state: bool) -> AppResult<()> {
        let mut register = self.get_holding(2)?;
        register.set_bit(3, state)?;
        self.set_holding(2, register)?;
        Ok(())
    }

    fn get_tank_lavel(&self) -> AppResult<u16> {
        let result = self.get_holding(3)?;
        Ok(result)
    }

    fn set_tank_lavel(&mut self, value: u16) -> AppResult<()> {
        self.set_holding(3, value)?;
        Ok(())
    }

    fn get_tank_temperature(&self) -> AppResult<f32> {
        let result = self.get_holdings_as_f32(4)?;
        Ok(result)
    }

    fn set_tank_temperature(&mut self, value: f32) -> AppResult<()> {
        self.set_holdings_from_f32(4, value)?;
        Ok(())
    }

    fn get_passive_cooling_state(&self) -> AppResult<bool> {
        let register = self.get_holding(6)?;
        let result = register.get_bit(0)?;
        Ok(result)
    }

    fn set_passive_cooling_state(&mut self, state: bool) -> AppResult<()> {
        let mut register = self.get_holding(6)?;
        register.set_bit(0, state)?;
        self.set_holding(6, register)?;
        Ok(())
    }

    fn get_active_cooling_state(&self) -> AppResult<bool> {
        let register = self.get_holding(6)?;
        let result = register.get_bit(1)?;
        Ok(result)
    }

    fn set_active_cooling_state(&mut self, state: bool) -> AppResult<()> {
        let mut register = self.get_holding(6)?;
        register.set_bit(1, state)?;
        self.set_holding(6, register)?;
        Ok(())
    }

    fn get_inside_temperature(&self) -> AppResult<f32> {
        let result = self.get_holdings_as_f32(7)?;
        Ok(result)
    }

    fn set_inside_temperature(&mut self, value: f32) -> AppResult<()> {
        self.set_holdings_from_f32(7, value)?;
        Ok(())
    }

    fn get_supply_air_temperature(&self) -> AppResult<f32> {
        let result = self.get_holdings_as_f32(9)?;
        Ok(result)
    }

    fn set_supply_air_temperature(&mut self, value: f32) -> AppResult<()> {
        self.set_holdings_from_f32(9, value)?;
        Ok(())
    }
}


