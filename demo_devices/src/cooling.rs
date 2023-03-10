use plc::task::MutProgram;
use crate::mb_context::{MbContext, AppResult};
use plc::pls_std::Rs;
use crate::DEBUG_LOG;

pub const SET_POINT_TEMP: f32 = 28.0;
pub const AIR_FLOW: f32 = 10000.0;
pub const COLLER_POWER: f32 = 60.0;

pub struct Cooling {
    passiv_cooling: Rs,
    active_cooling: Rs,
}

impl Cooling {
    pub fn new() -> Self {
        Self {
            passiv_cooling: Rs::new(),
            active_cooling: Rs::new(),
        }
    }

    fn passiv_cooling(&mut self, context: &mut plc::ModbusContext) -> AppResult<()> {
        let inside_temp = context.get_inside_temperature()?;
        
        let set = inside_temp >= (SET_POINT_TEMP - 2.0);
        let reset = inside_temp <= (SET_POINT_TEMP - 8.0);
        self.passiv_cooling.run(set, reset);
        context.set_passive_cooling_state(self.passiv_cooling.get_q())?;
        
        Ok(())
    }

    fn active_cooling(&mut self, context: &mut plc::ModbusContext) -> AppResult<()> {
        if !context.get_passive_cooling_state()? {
            context.set_active_cooling_state(false)?;
            return Ok(());
        }

        let inside_temp = context.get_inside_temperature()?;
        let set = inside_temp >= SET_POINT_TEMP;
        let reset = inside_temp <= (SET_POINT_TEMP - 4.0);

        self.active_cooling.run(set, reset);
        context.set_active_cooling_state(self.active_cooling.get_q())?;

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

            let active = context.get_active_cooling_state()?;
            let passive = context.get_passive_cooling_state()?;
            println!("Cooler: active {}, passive: {}", active, passive);
        }
        
        Ok(())
    }
}

impl MutProgram for Cooling {
    fn run(&mut self, context: &mut plc::ModbusContext) -> AppResult<()> {
        self.passiv_cooling(context)?;
        self.active_cooling(context)?;
        self.log(context)?;
        Ok(())
    }
}
