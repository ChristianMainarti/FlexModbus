using System;
using System.Threading;
using FlexModbus.Enums;
using FlexModbus.Interfaces;
using FlexModbus.Modbus;

namespace FlexModbus
{
    internal class Program
    {
		private ModbusConnection modbusConn;

		private ETypeReqModbus typeRequestModbus = ETypeReqModbus.TCPIP;

		public void ToggleStatusSingleCoil()
		{
			while (true)
			{
				try
				{
					if (!modbusConn.StatusConnection())
					{
						new ModbusConnection("192.168.2.199", 502, 1000, 500, 1000).OpenConnection();
					}
					else
					{
						IModbusClient instance = ModbusClient.GetInstance(modbusConn, typeRequestModbus);
						int[] valuesToWrite = new int[2] { 16896, 0 };
						instance.SendPresetMultipleRegisters(10, 1523, valuesToWrite);
					}
				}
				catch (Exception)
				{
					throw;
				}
				Thread.Sleep(1000);
			}
		}
	}
}
