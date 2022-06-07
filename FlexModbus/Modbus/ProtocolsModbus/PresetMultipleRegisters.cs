using System;
using System.Linq;
using FlexModbus.Modbus.Utils;

namespace FlexModbus.Modbus.ProtocolsModbus
{
	internal class PresetMultipleRegisters
	{
		private static readonly byte FunctionModbus = 16;

		public static Tuple<byte[], int> MountData(int _addressDevice, int _firstRegister, int[] _valuesToWrite)
		{
			int item = 8;
			int num = _valuesToWrite.Length * 2;
			byte[] array = new byte[num];
			for (int i = 0; i < _valuesToWrite.Length; i++)
			{
				array[i * 2] = BitsOperators.HighByte(_valuesToWrite[i]);
				array[i * 2 + 1] = BitsOperators.LowByte(_valuesToWrite[i]);
			}
			byte[] source = new byte[7]
			{
			BitsOperators.LowByte(_addressDevice),
			FunctionModbus,
			BitsOperators.HighByte(_firstRegister),
			BitsOperators.LowByte(_firstRegister),
			BitsOperators.HighByte(_valuesToWrite.Length),
			BitsOperators.LowByte(_valuesToWrite.Length),
			(byte)num
			};
			source = source.ToList().Concat(array.ToList()).ToArray();
			CheckSum.MountBuffer(ref source);
			return new Tuple<byte[], int>(source, item);
		}

		public static Tuple<byte[], int> MountData(int _transactionID, int _addressDevice, int _firstRegister, int[] _valuesToWrite)
		{
			int item = 12;
			int num = _valuesToWrite.Length * 2;
			byte[] array = new byte[num];
			for (int i = 0; i < _valuesToWrite.Length; i++)
			{
				array[i * 2] = BitsOperators.HighByte(_valuesToWrite[i]);
				array[i * 2 + 1] = BitsOperators.LowByte(_valuesToWrite[i]);
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
			BitsOperators.HighByte(_valuesToWrite.Length),
			BitsOperators.LowByte(_valuesToWrite.Length),
			(byte)num
			}.ToList().Concat(array.ToList()).ToArray(), item);
		}
	}
}
