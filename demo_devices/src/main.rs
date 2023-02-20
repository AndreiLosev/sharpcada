mod outside_temperature;
mod mb_context;

use crate::outside_temperature::OutsideTemperature;
use plc::Plc;
use plc::task::Task;
use plc::task::Program;

fn main() {
    let mut outside_temperature = OutsideTemperature::new();
    let mut programs = [Program::Mut(&mut outside_temperature)];
    let cycle = std::time::Duration::from_millis(100);
    let main_task = Task::new_cycle("main", &mut programs, 1, cycle);
    let tasks = [main_task];
    Plc::new(tasks).run();
}
