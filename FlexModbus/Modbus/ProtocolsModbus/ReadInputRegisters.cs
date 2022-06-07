using FlexModbus.Modbus.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexModbus.Modbus.ProtocolsModbus
{
	internal static class ReadInputRegisters
	{
		private static readonly byte FunctionModbus = 4;

		public static Tuple<byte[], int> MountData(int _addressDevice, int _firstRegister, int _quantityRegisters)
		{
			int num = _quantityRegisters * 2;
			int item = 5 + num;
			byte[] buffer = new byte[6]
			{
			BitsOperators.LowByte(_addressDevice),
			FunctionModbus,
			BitsOperators.HighByte(_firstRegister),
			BitsOperators.LowByte(_firstRegister),
			BitsOperators.HighByte(_quantityRegisters),
			BitsOperators.LowByte(_quantityRegisters)
			};
			CheckSum.MountBuffer(ref buffer);
			return new Tuple<byte[], int>(buffer, item);
		}

		public static Tuple<byte[], int> MountData(int _transactionID, int _addressDevice, int _firstRegister, int _quantityRegisters)
		{
			int num = _quantityRegisters * 2;
			int item = 9 + num;
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
			BitsOperators.HighByte(_quantityRegisters),
			BitsOperators.LowByte(_quantityRegisters)
			}, item);
		}
	}

}
