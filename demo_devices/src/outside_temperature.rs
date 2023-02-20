use plc::task::MutProgram;
use crate::mb_context::MbContext;
use crate::mb_context::AppResult;

pub struct OutsideTemperature {
    init_outside_temperature: f32,
    init: bool,
    time: std::time::Instant,
}

impl OutsideTemperature {
    pub fn new() -> Self {
        let init_outside_temperature = 15.0 + rand::random::<f32>() * 15.0;
        let time = std::time::Instant::now();
        Self { init_outside_temperature, time, init: false }
    }

    fn init(&mut self, context: &mut plc::ModbusContext) -> AppResult<()> {
        if !self.init {
            context.set_outside_temperature(self.init_outside_temperature)?;
            self.init = true;
        }
        Ok(())
    }

    fn log(&self, context: &mut plc::ModbusContext) -> AppResult<()> {
        let dt = self.time.elapsed().as_secs();
        if dt % 2 == 0 {
            let temp = context.get_outside_temperature()?;
            let dt = self.time.elapsed().as_secs_f32() / 20.0;
            println!("t: {}, sin: {}", temp, dt.sin());
        }
        Ok(())
    }
}


impl MutProgram for OutsideTemperature {
    fn run(&mut self, context: &mut plc::ModbusContext) -> AppResult<()> {
        self.init(context)?;

        let dt = self.time.elapsed().as_secs_f32() / 20.0;
        let outside_temperature = context.get_outside_temperature()?;
        let new_outside_temp = outside_temperature
            + (self.init_outside_temperature / 200.0) * rand::random::<f32>() * dt.sin();
        context.set_outside_temperature(new_outside_temp)?;

        self.log(context)?;

        Ok(())
    }
}
