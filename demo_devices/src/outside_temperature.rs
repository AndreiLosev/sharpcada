use plc::task::ConstProgram;
use crate::mb_context::MbContext;
use crate::mb_context::AppResult;
use crate::DEBUG_LOG;

pub struct OutsideTemperature {
    time: std::time::Instant,
}

impl OutsideTemperature {
    pub fn new() -> Self {
        let time = std::time::Instant::now();
        Self { time }
    }

    fn log(&self, context: &mut plc::ModbusContext) -> AppResult<()> {

        if !DEBUG_LOG {
            return Ok(());
        }

        let dt = self.time.elapsed().as_millis();

        if dt % 1000 < 50 {
            let temp = context.get_outside_temperature()?;
            println!("OutsideTemperature: {}", temp);
        }
        Ok(())
    }
}


impl ConstProgram for OutsideTemperature {
    fn run(&self, context: &mut plc::ModbusContext) -> AppResult<()> {

        let dt = self.time.elapsed().as_secs_f32() / 20.0;
        let outside_temperature = context.get_outside_temperature()?;
        let new_outside_temp = outside_temperature + 0.05 * rand::random::<f32>() * dt.sin();
        context.set_outside_temperature(new_outside_temp)?;

        self.log(context)?;

        Ok(())
    }
}
