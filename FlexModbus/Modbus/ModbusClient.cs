using FlexModbus.Interfaces;
using FlexModbus.Enums;

namespace FlexModbus.Modbus
{
    internal class ModbusClient
    {

		public static int QuantityAttempts = 3;

		public static IModbusClient GetInstance(ModbusConnection _modbusConnection, ETypeReqModbus _typeReqModbus)
		{
			return _typeReqModbus switch
			{
				ETypeReqModbus.TCPIP => new ModbusTCPIP(_modbusConnection),
				ETypeReqModbus.RTU => new ModbusRTU(_modbusConnection),
				_ => new ModbusRTU(_modbusConnection),
			};
		}
	}
}
