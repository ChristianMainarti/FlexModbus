using System;
using FlexModbus.Modbus.ProtocolsModbus;

namespace FlexModbus.Modbus.Utils
{
	internal static class MountProtocolsModbus
	{
		public static Tuple<byte[], int> FC_01(int _addressDevice, int _firstRegister, int _quantityRegisters)
		{
			return ReadStatusCoils.MountData(_addressDevice, _firstRegister, _quantityRegisters);
		}

		public static Tuple<byte[], int> FC_01(int _transactionID, int _addressDevice, int _firstRegister, int _quantityRegisters)
		{
			return ReadStatusCoils.MountData(_transactionID, _addressDevice, _firstRegister, _quantityRegisters);
		}

		public static Tuple<byte[], int> FC_02(int _addressDevice, int _firstRegister, int _quantityRegisters)
		{
			return ReadStatusDigitalInputs.MountData(_addressDevice, _firstRegister, _quantityRegisters);
		}

		public static Tuple<byte[], int> FC_02(int _transactionID, int _addressDevice, int _firstRegister, int _quantityRegisters)
		{
			return ReadStatusDigitalInputs.MountData(_transactionID, _addressDevice, _firstRegister, _quantityRegisters);
		}

		public static Tuple<byte[], int> FC_03(int _addressDevice, int _firstRegister, int _quantityRegisters)
		{
			return ReadHoldingRegisters.MountData(_addressDevice, _firstRegister, _quantityRegisters);
		}

		public static Tuple<byte[], int> FC_03(int _transactionID, int _addressDevice, int _firstRegister, int _quantityRegisters)
		{
			return ReadHoldingRegisters.MountData(_transactionID, _addressDevice, _firstRegister, _quantityRegisters);
		}

		public static Tuple<byte[], int> FC_04(int _addressDevice, int _firstRegister, int _quantityRegisters)
		{
			return ReadInputRegisters.MountData(_addressDevice, _firstRegister, _quantityRegisters);
		}

		public static Tuple<byte[], int> FC_04(int _transactionID, int _addressDevice, int _firstRegister, int _quantityRegisters)
		{
			return ReadInputRegisters.MountData(_transactionID, _addressDevice, _firstRegister, _quantityRegisters);
		}

		public static Tuple<byte[], int> FC_05(int _addressDevice, int _firstRegister, bool _statusToWrite)
		{
			return ForceSingleCoil.MountData(_addressDevice, _firstRegister, _statusToWrite);
		}

		public static Tuple<byte[], int> FC_05(int _transactionID, int _addressDevice, int _firstRegister, bool _statusToWrite)
		{
			return ForceSingleCoil.MountData(_transactionID, _addressDevice, _firstRegister, _statusToWrite);
		}

		public static Tuple<byte[], int> FC_06(int _addressDevice, int _firstRegister, int _valueToWrite)
		{
			return PresetSingleRegister.MountData(_addressDevice, _firstRegister, _valueToWrite);
		}

		public static Tuple<byte[], int> FC_06(int _transactionID, int _addressDevice, int _firstRegister, int _valueToWrite)
		{
			return PresetSingleRegister.MountData(_transactionID, _addressDevice, _firstRegister, _valueToWrite);
		}

		public static Tuple<byte[], int> FC_15(int _addressDevice, int _firstRegister, bool[] _valuesToWrite)
		{
			return ForceMultipleCoils.MountData(_addressDevice, _firstRegister, _valuesToWrite);
		}

		public static Tuple<byte[], int> FC_15(int _transactionID, int _addressDevice, int _firstRegister, bool[] _valuesToWrite)
		{
			return ForceMultipleCoils.MountData(_transactionID, _addressDevice, _firstRegister, _valuesToWrite);
		}

		public static Tuple<byte[], int> FC_16(int _addressDevice, int _firstRegister, int[] _valuesToWrite)
		{
			return PresetMultipleRegisters.MountData(_addressDevice, _firstRegister, _valuesToWrite);
		}

		public static Tuple<byte[], int> FC_16(int _transactionID, int _addressDevice, int _firstRegister, int[] _valuesToWrite)
		{
			return PresetMultipleRegisters.MountData(_transactionID, _addressDevice, _firstRegister, _valuesToWrite);
		}
	}
}