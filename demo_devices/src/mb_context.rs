use plc::ModbusContext;
use std::error::Error;

pub type  AppResult<T> = Result<T, Box<dyn Error>>;

pub trait MbContext {
    fn get_outside_temperature(&self) -> AppResult<f32>;
    fn set_outside_temperature(&mut self, value: f32) -> AppResult<()>;
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
}


