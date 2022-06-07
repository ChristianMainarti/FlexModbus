using System;
using System.Linq;
using FlexModbus.Modbus;
using FlexModbus.Modbus.Utils;

namespace FlexModbus.Modbus.ProtocolsModbus
{
    class ForceMultipleCoils
    {
		private static readonly byte FunctionModbus = 15;

		public static Tuple<byte[], int> MountData(int _addressDevice, int _firstRegister, bool[] _statusToWrite)
		{
			int item = 8;
			int num = (int)((Math.Ceiling((decimal)_statusToWrite.Length / 8.0m) > 1m) ? Math.Ceiling((decimal)_statusToWrite.Length / 8.0m) : 1m);
			byte[] array = new byte[num];
			int num2 = 0;
			for (int i = 0; i < _statusToWrite.Length; i++)
			{
				if (i % 8 == 0 && i != 0)
				{
					num2++;
				}
				array[num2] |= (byte)(Convert.ToByte(_statusToWrite[i]) << i - num2 * 8);
			}
			byte[] source = new byte[7]
			{
			BitsOperators.LowByte(_addressDevice),
			FunctionModbus,
			BitsOperators.HighByte(_firstRegister),
			BitsOperators.LowByte(_firstRegister),
			BitsOperators.HighByte(_statusToWrite.Length),
			BitsOperators.LowByte(_statusToWrite.Length),
			(byte)num
			};
			source = source.ToList().Concat(array.ToList()).ToArray();
			CheckSum.MountBuffer(ref source);
			return new Tuple<byte[], int>(source, item);
		}

		public static Tuple<byte[], int> MountData(int _transactionID, int _addressDevice, int _firstRegister, bool[] _statusToWrite)
		{
			int item = 12;
			int num = (int)((Math.Ceiling((decimal)_statusToWrite.Length / 8.0m) > 1m) ? Math.Ceiling((decimal)_statusToWrite.Length / 8.0m) : 1m);
			byte[] array = new byte[num];
			int num2 = 0;
			for (int i = 0; i < _statusToWrite.Length; i++)
			{
				if (i % 8 == 0 && i != 0)
				{
					num2++;
				}
				array[num2] |= (byte)(Convert.ToByte(_statusToWrite[i]) << i - num2 * 8);
			}
			return new Tuple<byte[], int>(new byte[13]
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
			BitsOperators.HighByte(_statusToWrite.Length),
			BitsOperators.LowByte(_statusToWrite.Length),
			(byte)num
			}.ToList().Concat(array.ToList()).ToArray(), item);
		}
	}
}
