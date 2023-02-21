mod outside_temperature;
mod mb_context;

use crate::outside_temperature::OutsideTemperature;
use plc::Plc;
use plc::task::Task;
use plc::task::Program;
use crate::mb_context::MbContext;
use plc::ModbusContext;

static DEBUG_LOG: bool = true;

fn main() {
    let outside_temperature = OutsideTemperature::new();
    let mut programs = [Program::Const(&outside_temperature)];

    let cycle = std::time::Duration::from_millis(100);
    let main_task = Task::new_cycle("main", &mut programs, 1, cycle);
    let tasks = [main_task];

    let context = init_context();

    Plc::new(tasks, context).run();
}


fn init_context() -> ModbusContext {
    let mut context = ModbusContext::new();
    let init_outside_temperature = 15.0 + rand::random::<f32>() * 15.0;
    context.set_outside_temperature(init_outside_temperature).unwrap();

    context
}
