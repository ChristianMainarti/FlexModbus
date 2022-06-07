﻿using FlexModbus.Modbus.Utils;
using System;


namespace FlexModbus.Modbus.ProtocolsModbus
{
	internal static class ReadStatusCoils
	{
		private static readonly byte FunctionModbus = 1;

		public static Tuple<byte[], int> MountData(int _addressDevice, int _firstRegister, int _quantityRegisters)
		{
			int num = (int)(((decimal)_quantityRegisters / 8m > 1m) ? Math.Ceiling((decimal)_quantityRegisters / 8m) : 1m);
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
			int num = (int)(((decimal)_quantityRegisters / 8m > 1m) ? Math.Ceiling((decimal)_quantityRegisters / 8m) : 1m);
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
