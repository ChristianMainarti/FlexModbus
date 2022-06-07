using System;
using FlexModbus.Modbus.Utils;

namespace FlexModbus.Modbus.ProtocolsModbus
{
    internal class ForceSingleCoil
    {

		private static readonly byte FunctionModbus = 5;

		public static Tuple<byte[], int> MountData(int _addressDevice, int _firstRegister, bool _statusToWrite)
		{
			int item = 8;
			byte[] buffer = new byte[6]
			{
			BitsOperators.LowByte(_addressDevice),
			FunctionModbus,
			BitsOperators.HighByte(_firstRegister),
			BitsOperators.LowByte(_firstRegister),
			(byte)(_statusToWrite ? 255u : 0u),
			0
			};
			CheckSum.MountBuffer(ref buffer);
			return new Tuple<byte[], int>(buffer, item);
		}

		public static Tuple<byte[], int> MountData(int _transactionID, int _addressDevice, int _firstRegister, bool _statusToWrite)
		{
			int item = 12;
			return new Tuple<byte[], int>(new byte[12]
			{
			BitsOperators.HighByte(_transactionID),
			BitsOperators.LowByte(_transactionID),
			0,
			0,
			0,
			6,
			BitsOperators.LowByte(_addressDevice),
			FunctionModbus,
			BitsOperators.HighByte(_firstRegister),
			BitsOperators.LowByte(_firstRegister),
			(byte)(_statusToWrite ? 255u : 0u),
			0
			}, item);
		}
	}
}
