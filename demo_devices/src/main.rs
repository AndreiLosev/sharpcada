mod outside_temperature;
mod mb_context;
mod tank_logic;
mod physics;
mod cooling;

use plc::pls_std::ToReg;
use crate::outside_temperature::OutsideTemperature;
use crate::tank_logic::{TankLogic, TANK_LOW_LEVEL};
use crate::cooling::Cooling;
use crate::physics::Physics;
use plc::Plc;
use plc::task::Task;
use plc::task::Program;
use crate::mb_context::MbContext;
use plc::ModbusContext;
use plc::system_prog::modbus::ModbusTcpSlave;

static DEBUG_LOG: bool = false;

fn main() {
    let outside_temperature = OutsideTemperature::new();
    let mut tank_logic = TankLogic::new();
    let mut coller = Cooling::new();
    let mut physics = Physics::new();
    let modbus = ModbusTcpSlave::new(1, "0.0.0.0:5020");

    let mut programs = [
        Program::Const(&outside_temperature),
        Program::Mut(&mut tank_logic),
        Program::Mut(&mut coller),
        Program::Mut(&mut physics),
    ];

    let mut network_prg = [Program::Const(&modbus)];
    let network_task = Task::new_background("networl", &mut network_prg, 1);

    let cycle = std::time::Duration::from_millis(100);
    let main_task = Task::new_cycle("main", &mut programs, 1, cycle);

    let tasks = [main_task, network_task];

    let context = init_context();

    Plc::new(tasks, context).run();
}


fn init_context() -> ModbusContext {
    let mut context = ModbusContext::new();

    let init_outside_temperature = 15.0 + rand::random::<f32>() * 15.0;

    context.set_outside_temperature(init_outside_temperature).unwrap();
    context.set_tank_lavel(TANK_LOW_LEVEL).unwrap();
    context.set_tank_temperature(init_outside_temperature + 5.0).unwrap();
    context.set_inside_temperature(init_outside_temperature + 5.0).unwrap();
    context.set_supply_air_temperature(init_outside_temperature).unwrap();

    context.set_holdings_from_u32(100, 99999).unwrap();
    
    let arr = 99999_u32.to_reg();
    context.set_holdings_bulk(100, &arr).unwrap();

    context
}
