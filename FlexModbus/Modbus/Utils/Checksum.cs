using System;
using System.Collections.Generic;
using System.Linq;

namespace FlexModbus.Modbus.Utils
{
	internal static class CheckSum
	{
		internal static ushort Compute(byte[] buffer)
		{
			ushort num = ushort.MaxValue;
			for (int i = 0; i < buffer.Length; i++)
			{
				num = (ushort)(num ^ buffer[i]);
				for (int num2 = 8; num2 != 0; num2--)
				{
					if (((uint)num & (true ? 1u : 0u)) != 0)
					{
						num = (ushort)(num >> 1);
						num = (ushort)(num ^ 0xA001u);
					}
					else
					{
						num = (ushort)(num >> 1);
					}
				}
			}
			return (ushort)((ushort)(num << 8) | (num >> 8));
		}

		internal static void MountBuffer(ref byte[] buffer)
		{
			ushort value = Compute(buffer);
			List<byte> list = buffer.ToList();
			list.Add(BitsOperators.HighByte(value));
			list.Add(BitsOperators.LowByte(value));
			buffer = list.ToArray();
		}

		internal static bool IsMathCheckSum(byte[] buffer)
		{
			ushort num = (ushort)((buffer[buffer.Length - 2] << 8) | buffer[buffer.Length - 1]);
			byte[] array = new byte[buffer.Length - 2];
			Array.Copy(buffer, array, buffer.Length - 2);
			ushort num2 = Compute(array);
			return num == num2;
		}
	}
}
