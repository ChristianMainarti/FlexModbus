using System;
using FlexModbus.Interfaces;
using FlexModbus.Enums;
using FlexModbus.Modbus.Utils;

namespace FlexModbus.Modbus
{
    internal class ModbusTCPIP : IModbusClient
    {
		private readonly int QuantityAttempts = 1;

		private readonly ModbusConnection ModbusConnection;

		private readonly ETypeReqModbus TypeReqModbus = ETypeReqModbus.TCPIP;

		public ModbusTCPIP(ModbusConnection _modbusConnection)
		{
			ModbusConnection = _modbusConnection;
			QuantityAttempts = ModbusClient.QuantityAttempts;
		}

		private bool IsValidMBAPHeader(byte[] buffer, byte[] response)
		{
			bool num = ((buffer[0] << 8) | buffer[1]) == ((response[0] << 8) | response[1]);
			bool flag = ((buffer[2] << 8) | buffer[3]) == ((response[2] << 8) | response[3]);
			bool flag2 = buffer[6] == response[6];
			bool flag3 = buffer[7] == response[7];
			return num && flag && flag2 && flag3;
		}

		public bool[] SendReadStatusCoils(int _addressDevice, int _firstRegister, int _quantityRegisters)
		{
			try
			{
				for (int i = 0; i < QuantityAttempts; i++)
				{
					MountProtocolsModbus.FC_01(ModbusConnection.GetTransactionID(), _addressDevice, _firstRegister, _quantityRegisters).Deconstruct(out var item, out var item2);
					byte[] array = item;
					int num = item2;
					byte[] array2 = ModbusConnection.SendRequest(array, num, TypeReqModbus);
					if (array2 == null || !IsValidMBAPHeader(array, array2))
					{
						continue;
					}
					bool num2 = (uint)((array2[4] << 8) | array2[5]) == num - 6;
					int num3 = ((num - 9 == array2[8]) ? array2[8] : 0);
					if (!num2 || num3 <= 0)
					{
						continue;
					}
					bool[] array3 = new bool[num3 * 8];
					for (int j = 0; j < num3; j++)
					{
						for (int k = 0; k < 8; k++)
						{
							array3[j * 8 + k] = BitsOperators.BitRead(array2[9 + j], k);
						}
					}
					bool[] array4 = new bool[_quantityRegisters];
					Array.Copy(array3, array4, _quantityRegisters);
					return array4;
				}
				throw new Exception("Failed to read status coils FC 01 - ModbusTCP/IP");
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public bool[] SendReadStatusDigitalStatus(int _addressDevice, int _firstRegister, int _quantityRegisters)
		{
			try
			{
				for (int i = 0; i < QuantityAttempts; i++)
				{
					MountProtocolsModbus.FC_02(ModbusConnection.GetTransactionID(), _addressDevice, _firstRegister, _quantityRegisters).Deconstruct(out var item, out var item2);
					byte[] array = item;
					int num = item2;
					byte[] array2 = ModbusConnection.SendRequest(array, num, TypeReqModbus);
					if (array2 == null || !IsValidMBAPHeader(array, array2))
					{
						continue;
					}
					bool num2 = (uint)((array2[4] << 8) | array2[5]) == num - 6;
					int num3 = ((num - 9 == array2[8]) ? array2[8] : 0);
					if (!num2 || num3 <= 0)
					{
						continue;
					}
					bool[] array3 = new bool[num3 * 8];
					for (int j = 0; j < num3; j++)
					{
						for (int k = 0; k < 8; k++)
						{
							array3[j * 8 + k] = BitsOperators.BitRead(array2[9 + j], k);
						}
					}
					bool[] array4 = new bool[_quantityRegisters];
					Array.Copy(array3, array4, _quantityRegisters);
					return array4;
				}
				throw new Exception("Failed to read digital inputs FC 02 - ModbusTCP/IP");
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public int[] SendReadHoldingRegisters(int _addressDevice, int _firstRegister, int _quantityRegisters)
		{
			try
			{
				for (int i = 0; i < QuantityAttempts; i++)
				{
					MountProtocolsModbus.FC_03(ModbusConnection.GetTransactionID(), _addressDevice, _firstRegister, _quantityRegisters).Deconstruct(out var item, out var item2);
					byte[] array = item;
					int num = item2;
					byte[] array2 = ModbusConnection.SendRequest(array, num, TypeReqModbus);
					if (array2 == null || !IsValidMBAPHeader(array, array2))
					{
						continue;
					}
					bool num2 = (uint)((array2[4] << 8) | array2[5]) == num - 6;
					int num3 = ((num - 9 == array2[8]) ? array2[8] : 0);
					if (num2 && num3 > 0)
					{
						int[] array3 = new int[num3 / 2];
						for (int j = 0; j < array3.Length; j++)
						{
							array3[j] = (array2[7 + (j + 1) * 2] << 8) | array2[7 + (j + 1) * 2 + 1];
						}
						return array3;
					}
				}
				throw new Exception("Failed to single content retentive FC 03 - ModbusTCP/IP");
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public int[] SendReadInputRegisters(int _addressDevice, int _firstRegister, int _quantityRegisters)
		{
			try
			{
				for (int i = 0; i < QuantityAttempts; i++)
				{
					MountProtocolsModbus.FC_04(ModbusConnection.GetTransactionID(), _addressDevice, _firstRegister, _quantityRegisters).Deconstruct(out var item, out var item2);
					byte[] array = item;
					int num = item2;
					byte[] array2 = ModbusConnection.SendRequest(array, num, TypeReqModbus);
					if (array2 == null || !IsValidMBAPHeader(array, array2))
					{
						continue;
					}
					bool num2 = (uint)((array2[4] << 8) | array2[5]) == num - 6;
					int num3 = ((num - 9 == array2[8]) ? array2[8] : 0);
					if (num2 && num3 > 0)
					{
						int[] array3 = new int[num3 / 2];
						for (int j = 0; j < array3.Length; j++)
						{
							array3[j] = (array2[7 + (j + 1) * 2] << 8) | array2[7 + (j + 1) * 2 + 1];
						}
						return array3;
					}
				}
				throw new Exception("Failed to read analog inputs FC 04 - ModbusTCP/IP");
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public bool SendForceSingleCoil(int _addressDevice, int _firstRegister, bool _statusToWrite)
		{
			try
			{
				for (int i = 0; i < QuantityAttempts; i++)
				{
					MountProtocolsModbus.FC_05(ModbusConnection.GetTransactionID(), _addressDevice, _firstRegister, _statusToWrite).Deconstruct(out var item, out var item2);
					byte[] array = item;
					int sizeBufferExpected = item2;
					byte[] array2 = ModbusConnection.SendRequest(array, sizeBufferExpected, TypeReqModbus);
					if (array2 != null && IsValidMBAPHeader(array, array2))
					{
						bool num = ((array2[4] << 8) | array2[5]) == 6;
						bool flag = (uint)((array2[8] << 8) | array2[9]) == _firstRegister;
						if (num && flag)
						{
							return array2[10] > 0;
						}
					}
				}
				throw new Exception("Failed to write in single coil FC 05 - ModbusTCP/IP");
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public int SendPresetSingleRegister(int _addressDevice, int _firstRegister, int _valueToWrite)
		{
			try
			{
				for (int i = 0; i < QuantityAttempts; i++)
				{
					MountProtocolsModbus.FC_06(ModbusConnection.GetTransactionID(), _addressDevice, _firstRegister, _valueToWrite).Deconstruct(out var item, out var item2);
					byte[] array = item;
					int sizeBufferExpected = item2;
					byte[] array2 = ModbusConnection.SendRequest(array, sizeBufferExpected, TypeReqModbus);
					if (array2 != null && IsValidMBAPHeader(array, array2))
					{
						bool num = ((array2[4] << 8) | array2[5]) == 6;
						bool flag = (uint)((array2[8] << 8) | array2[9]) == _firstRegister;
						if (num && flag)
						{
							return (array2[10] << 8) | array2[11];
						}
					}
				}
				throw new Exception("Failed to preset single register FC 06 - ModbusTCP/IP");
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public bool SendForceMultipleCoils(int _addressDevice, int _firstRegister, bool[] _valueToWrite)
		{
			try
			{
				for (int i = 0; i < QuantityAttempts; i++)
				{
					MountProtocolsModbus.FC_15(ModbusConnection.GetTransactionID(), _addressDevice, _firstRegister, _valueToWrite).Deconstruct(out var item, out var item2);
					byte[] array = item;
					int sizeBufferExpected = item2;
					byte[] array2 = ModbusConnection.SendRequest(array, sizeBufferExpected, TypeReqModbus);
					if (array2 != null && IsValidMBAPHeader(array, array2))
					{
						bool num = ((array2[4] << 8) | array2[5]) == 6;
						bool flag = (uint)((array2[8] << 8) | array2[9]) == _firstRegister;
						bool flag2 = (uint)((array2[10] << 8) | array2[11]) == _valueToWrite.Length;
						if (num && flag && flag2)
						{
							return true;
						}
					}
				}
				throw new Exception("Failed to write status into coils FC 15 - ModbusTCP/IP");
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public bool SendPresetMultipleRegisters(int _addressDevice, int _firstRegister, int[] _valuesToWrite)
		{
			try
			{
				for (int i = 0; i < QuantityAttempts; i++)
				{
					MountProtocolsModbus.FC_16(ModbusConnection.GetTransactionID(), _addressDevice, _firstRegister, _valuesToWrite).Deconstruct(out var item, out var item2);
					byte[] array = item;
					int sizeBufferExpected = item2;
					byte[] array2 = ModbusConnection.SendRequest(array, sizeBufferExpected, TypeReqModbus);
					if (array2 != null && IsValidMBAPHeader(array, array2))
					{
						bool num = ((array2[4] << 8) | array2[5]) == 6;
						bool flag = (uint)((array2[8] << 8) | array2[9]) == _firstRegister;
						bool flag2 = (uint)((array2[10] << 8) | array2[11]) == _valuesToWrite.Length;
						if (num && flag && flag2)
						{
							return true;
						}
					}
				}
				throw new Exception("Failed preset values to multiples registers FC 16 - ModbusTCP/IP");
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
	}
}
