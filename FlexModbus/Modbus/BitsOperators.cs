using System;

namespace FlexModbus.Modbus
{
    internal class BitsOperators
    {
		public static bool BitRead(byte buffer, int pos)
		{
			if (pos > 7)
			{
				throw new Exception("Invalid position for read from buffer!");
			}
			return (buffer & (1 << (int)(byte)pos)) >= 1;
		}

		public static void BitWrite(ref byte buffer, int pos, bool value)
		{
			if (pos > 7)
			{
				throw new Exception("Invalid position for write in the buffer!");
			}
			if (value)
			{
				buffer |= (byte)(1 << pos);
			}
			else
			{
				buffer &= (byte)(~(1 << pos));
			}
		}

		public static byte LowByte(int value)
		{
			return (byte)((uint)value & 0xFFu);
		}

		public static byte HighByte(int value)
		{
			return (byte)(value >> 8);
		}
	}
}
