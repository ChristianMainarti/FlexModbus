using System;
using FlexModbus.Interfaces;
using FlexModbus.Enums;
using FlexModbus.Modbus.Utils;

namespace FlexModbus.Modbus
{
    internal class ModbusRTU : IModbusClient
    {
		private readonly int QuantityAttempts = 1;

		private readonly ModbusConnection ModbusConnection;

		private readonly ETypeReqModbus TypeReqModbus = ETypeReqModbus.RTU;

		public ModbusRTU(ModbusConnection _modbusConnection)
		{
			ModbusConnection = _modbusConnection;
			QuantityAttempts = ModbusClient.QuantityAttempts;
		}

		internal bool IsValidRTU(byte[] buffer, byte[] response)
		{
			bool num = buffer[0] == response[0];
			bool flag = buffer[1] == response[1];
			bool flag2 = CheckSum.IsMathCheckSum(response);
			return num && flag && flag2;
		}

		public bool[] SendReadStatusCoils(int _addressDevice, int _firstRegister, int _quantityRegisters)
		{
			try
			{
				for (int i = 0; i < QuantityAttempts; i++)
				{
					MountProtocolsModbus.FC_01(_addressDevice, _firstRegister, _quantityRegisters).Deconstruct(out var item, out var item2);
					byte[] array = item;
					int num = item2;
					byte[] array2 = ModbusConnection.SendRequest(array, num, TypeReqModbus);
					if (array2 == null || !IsValidRTU(array, array2))
					{
						continue;
					}
					int num2 = ((num - 5 == array2[2]) ? array2[2] : 0);
					if (num2 <= 0)
					{
						continue;
					}
					bool[] array3 = new bool[num2 * 8];
					for (int j = 0; j < num2; j++)
					{
						for (int k = 0; k < 8; k++)
						{
							array3[j * 8 + k] = BitsOperators.BitRead(array2[3 + j], k);
						}
					}
					return array3;
				}
				throw new Exception("Failed to read status coils FC 01 - ModbusRTU");
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
					MountProtocolsModbus.FC_02(_addressDevice, _firstRegister, _quantityRegisters).Deconstruct(out var item, out var item2);
					byte[] array = item;
					int num = item2;
					byte[] array2 = ModbusConnection.SendRequest(array, num, TypeReqModbus);
					if (array2 == null || !IsValidRTU(array, array2))
					{
						continue;
					}
					int num2 = ((num - 5 == array2[2]) ? array2[2] : 0);
					if (num2 <= 0)
					{
						continue;
					}
					bool[] array3 = new bool[num2 * 8];
					for (int j = 0; j < num2; j++)
					{
						for (int k = 0; k < 8; k++)
						{
							array3[j * 8 + k] = BitsOperators.BitRead(array2[3 + j], k);
						}
					}
					return array3;
				}
				throw new Exception("Failed to read digital inputs FC 02 - ModbusRTU");
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
					MountProtocolsModbus.FC_03(_addressDevice, _firstRegister, _quantityRegisters).Deconstruct(out var item, out var item2);
					byte[] array = item;
					int num = item2;
					byte[] array2 = ModbusConnection.SendRequest(array, num, TypeReqModbus);
					if (array2 == null || !IsValidRTU(array, array2))
					{
						continue;
					}
					int num2 = ((num - 5 == array2[2]) ? array2[2] : 0);
					if (num2 > 0)
					{
						int[] array3 = new int[num2 / 2];
						for (int j = 0; j < array3.Length; j++)
						{
							array3[j] = (array2[1 + (j + 1) * 2] << 8) | array2[1 + (j + 1) * 2 + 1];
						}
						return array3;
					}
				}
				throw new Exception("Failed to single content retentive FC 03 - ModbusRTU");
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
					MountProtocolsModbus.FC_04(_addressDevice, _firstRegister, _quantityRegisters).Deconstruct(out var item, out var item2);
					byte[] array = item;
					int num = item2;
					byte[] array2 = ModbusConnection.SendRequest(array, num, TypeReqModbus);
					if (array2 == null || !IsValidRTU(array, array2))
					{
						continue;
					}
					int num2 = ((num - 5 == array2[2]) ? array2[2] : 0);
					if (num2 > 0)
					{
						int[] array3 = new int[num2 / 2];
						for (int j = 0; j < array3.Length; j++)
						{
							array3[j] = (array2[1 + (j + 1) * 2] << 8) | array2[1 + (j + 1) * 2 + 1];
						}
						return array3;
					}
				}
				throw new Exception("Failed to read analog inputs FC 04 - ModbusRTU");
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
					MountProtocolsModbus.FC_05(_addressDevice, _firstRegister, _statusToWrite).Deconstruct(out var item, out var item2);
					byte[] array = item;
					int sizeBufferExpected = item2;
					byte[] array2 = ModbusConnection.SendRequest(array, sizeBufferExpected, TypeReqModbus);
					if (array2 != null && IsValidRTU(array, array2) && ((array2[2] << 8) | array2[3]) == _firstRegister)
					{
						return array2[4] > 0;
					}
				}
				throw new Exception("Failed to write in single coil FC 05 - ModbusRTU");
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
					MountProtocolsModbus.FC_06(_addressDevice, _firstRegister, _valueToWrite).Deconstruct(out var item, out var item2);
					byte[] array = item;
					int sizeBufferExpected = item2;
					byte[] array2 = ModbusConnection.SendRequest(array, sizeBufferExpected, TypeReqModbus);
					if (array2 != null && IsValidRTU(array, array2) && ((array2[2] << 8) | array2[3]) == _firstRegister)
					{
						return (array2[4] << 8) | array2[5];
					}
				}
				throw new Exception("Failed to preset single register FC 06 - ModbusRTU");
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
					MountProtocolsModbus.FC_15(_addressDevice, _firstRegister, _valueToWrite).Deconstruct(out var item, out var item2);
					byte[] array = item;
					int sizeBufferExpected = item2;
					byte[] array2 = ModbusConnection.SendRequest(array, sizeBufferExpected, TypeReqModbus);
					if (array2 != null && IsValidRTU(array, array2))
					{
						bool num = (uint)((array2[2] << 8) | array2[3]) == _firstRegister;
						bool flag = (uint)((array2[4] << 8) | array2[5]) == _valueToWrite.Length;
						if (num && flag)
						{
							return true;
						}
					}
				}
				throw new Exception("Failed to write status into coils FC 15 - ModbusRTU");
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
					MountProtocolsModbus.FC_16(_addressDevice, _firstRegister, _valuesToWrite).Deconstruct(out var item, out var item2);
					byte[] array = item;
					int sizeBufferExpected = item2;
					byte[] array2 = ModbusConnection.SendRequest(array, sizeBufferExpected, TypeReqModbus);
					if (array2 != null && IsValidRTU(array, array2))
					{
						bool num = (uint)((array2[2] << 8) | array2[3]) == _firstRegister;
						bool flag = (uint)((array2[4] << 8) | array2[5]) == _valuesToWrite.Length;
						if (num && flag)
						{
							return true;
						}
					}
				}
				throw new Exception("Failed preset values to multiples registers FC 16 - ModbusRTU");
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
	}
}
