use crate::mb_context::{AppResult, MbContext};
use plc::pls_std::{Rs, Ton};
use plc::task::MutProgram;
use std::time::Duration;

pub struct TankLogic {
    state: TankLogicState,
    heater: Rs,
    timer: Ton,
}

pub const TANK_LOW_LEVEL: u16 = 6500;
pub const TANK_TOP_LEVEL: u16 = 27767;
pub const TANK_WORK_TEMP: f32 = 95.0;
pub const TANK_EMPTING_TEMP: f32 = 40.0;
pub const TANK_TIME_EXPOSURE: Duration = Duration::from_secs(150);

impl TankLogic {
    pub fn new() -> Self {
        Self {
            state: TankLogicState::Stop,
            heater: Rs::new(),
            timer: Ton::new(TANK_TIME_EXPOSURE),
        }
    }

    fn need_start(&mut self, context: &mut plc::ModbusContext) -> AppResult<()> {
        let need_start = context.get_tank_run()?;
        if need_start {
            self.state = TankLogicState::Filling;
        }

        Ok(())
    }

    fn filling(&mut self, context: &mut plc::ModbusContext) -> AppResult<()> {
        let tank_level = context.get_tank_lavel()?;

        if tank_level < TANK_TOP_LEVEL {
            context.set_filling_valve_state(true)?;
        }

        context.set_filling_valve_state(false)?;
        self.state = TankLogicState::Heat;

        Ok(())
    }

    fn heat(&mut self, context: &mut plc::ModbusContext, run: bool) -> AppResult<()> {
        let tank_temp = context.get_tank_temperature()?;
        let set = tank_temp <= (TANK_WORK_TEMP - 1.0);
        let reset = tank_temp >= (TANK_WORK_TEMP + 1.0);
        self.heater.run(set, reset);

        context.set_tank_heater(self.heater.get_q() && run)?;

        if let TankLogicState::Heat = self.state {
            if tank_temp >= TANK_WORK_TEMP {
                self.state = TankLogicState::TimeExposure;
            }
        }

        Ok(())
    }

    fn time_exposure(&mut self, context: &mut plc::ModbusContext) -> AppResult<()> {
        self.timer.run(true);
        self.heat(context, !self.timer.get_q())?;

        if self.timer.get_q() {
            self.state = TankLogicState::CooledDown;
        }

        Ok(())
    }

    fn cooled_down(&mut self, context: &mut plc::ModbusContext) -> AppResult<()> {
        let tank_temp = context.get_tank_temperature()?;
        if tank_temp <= TANK_EMPTING_TEMP {
            self.state = TankLogicState::Emptying;
        }

        Ok(())
    }

    fn emptying(&mut self, context: &mut plc::ModbusContext) -> AppResult<()> {
        context.set_drain_valve_state(true)?;
        let tank_level = context.get_tank_lavel()?;

        if tank_level <= TANK_LOW_LEVEL {
            context.set_drain_valve_state(false)?;
            context.set_tank_run(false)?;
            self.state = TankLogicState::Stop;
        }

        Ok(())
    }
}

impl MutProgram for TankLogic {
    fn run(&mut self, context: &mut plc::ModbusContext) -> AppResult<()> {
        match self.state {
            TankLogicState::Stop => self.need_start(context)?,
            TankLogicState::Filling => self.filling(context)?,
            TankLogicState::Heat => self.heat(context, true)?,
            TankLogicState::TimeExposure => self.time_exposure(context)?,
            TankLogicState::CooledDown => self.cooled_down(context)?,
            TankLogicState::Emptying => self.emptying(context)?,
        };

        Ok(())
    }
}

enum TankLogicState {
    Stop,
    Filling,
    Heat,
    TimeExposure,
    CooledDown,
    Emptying,
}
