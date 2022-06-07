using FlexModbus.Enums;
using System;
using System.Text;

namespace FlexModbus.Modbus
{
    internal class ConvertersModbus
    {

		public static float ConvertRegistersToFloat(int[] registers)
		{
			if (registers.Length != 2)
			{
				throw new ArgumentException("Input Array length invalid - Array langth must be '2'");
			}
			int value = registers[1];
			int value2 = registers[0];
			byte[] bytes = BitConverter.GetBytes(value);
			byte[] bytes2 = BitConverter.GetBytes(value2);
			return BitConverter.ToSingle(new byte[4]
			{
			bytes2[0],
			bytes2[1],
			bytes[0],
			bytes[1]
			}, 0);
		}

		public static float ConvertRegistersToFloat(int[] registers, RegisterOrder registerOrder)
		{
			int[] registers2 = new int[2]
			{
			registers[0],
			registers[1]
			};
			if (registerOrder == RegisterOrder.HighLow)
			{
				registers2 = new int[2]
				{
				registers[1],
				registers[0]
				};
			}
			return ConvertRegistersToFloat(registers2);
		}

		public static int ConvertRegistersToInt(int[] registers)
		{
			if (registers.Length != 2)
			{
				throw new ArgumentException("Input Array length invalid - Array langth must be '2'");
			}
			int value = registers[1];
			int value2 = registers[0];
			byte[] bytes = BitConverter.GetBytes(value);
			byte[] bytes2 = BitConverter.GetBytes(value2);
			return BitConverter.ToInt32(new byte[4]
			{
			bytes2[0],
			bytes2[1],
			bytes[0],
			bytes[1]
			}, 0);
		}

		public static int ConvertRegistersToInt(int[] registers, RegisterOrder registerOrder)
		{
			int[] registers2 = new int[2]
			{
			registers[0],
			registers[1]
			};
			if (registerOrder == RegisterOrder.HighLow)
			{
				registers2 = new int[2]
				{
				registers[1],
				registers[0]
				};
			}
			return ConvertRegistersToInt(registers2);
		}

		public static long ConvertRegistersToLong(int[] registers)
		{
			if (registers.Length != 4)
			{
				throw new ArgumentException("Input Array length invalid - Array langth must be '4'");
			}
			int value = registers[3];
			int value2 = registers[2];
			int value3 = registers[1];
			int value4 = registers[0];
			byte[] bytes = BitConverter.GetBytes(value);
			byte[] bytes2 = BitConverter.GetBytes(value2);
			byte[] bytes3 = BitConverter.GetBytes(value3);
			byte[] bytes4 = BitConverter.GetBytes(value4);
			return BitConverter.ToInt64(new byte[8]
			{
			bytes4[0],
			bytes4[1],
			bytes3[0],
			bytes3[1],
			bytes2[0],
			bytes2[1],
			bytes[0],
			bytes[1]
			}, 0);
		}

		public static long ConvertRegistersToLong(int[] registers, RegisterOrder registerOrder)
		{
			if (registers.Length != 4)
			{
				throw new ArgumentException("Input Array length invalid - Array langth must be '4'");
			}
			int[] registers2 = new int[4]
			{
			registers[0],
			registers[1],
			registers[2],
			registers[3]
			};
			if (registerOrder == RegisterOrder.HighLow)
			{
				registers2 = new int[4]
				{
				registers[3],
				registers[2],
				registers[1],
				registers[0]
				};
			}
			return ConvertRegistersToLong(registers2);
		}

		public static double ConvertRegistersToDouble(int[] registers)
		{
			if (registers.Length != 4)
			{
				throw new ArgumentException("Input Array length invalid - Array langth must be '4'");
			}
			int value = registers[3];
			int value2 = registers[2];
			int value3 = registers[1];
			int value4 = registers[0];
			byte[] bytes = BitConverter.GetBytes(value);
			byte[] bytes2 = BitConverter.GetBytes(value2);
			byte[] bytes3 = BitConverter.GetBytes(value3);
			byte[] bytes4 = BitConverter.GetBytes(value4);
			return BitConverter.ToDouble(new byte[8]
			{
			bytes4[0],
			bytes4[1],
			bytes3[0],
			bytes3[1],
			bytes2[0],
			bytes2[1],
			bytes[0],
			bytes[1]
			}, 0);
		}

		public static double ConvertRegistersToDouble(int[] registers, RegisterOrder registerOrder)
		{
			if (registers.Length != 4)
			{
				throw new ArgumentException("Input Array length invalid - Array langth must be '4'");
			}
			int[] registers2 = new int[4]
			{
			registers[0],
			registers[1],
			registers[2],
			registers[3]
			};
			if (registerOrder == RegisterOrder.HighLow)
			{
				registers2 = new int[4]
				{
				registers[3],
				registers[2],
				registers[1],
				registers[0]
				};
			}
			return ConvertRegistersToDouble(registers2);
		}

		public static int[] ConvertFloatToRegisters(float floatValue)
		{
			byte[] bytes = BitConverter.GetBytes(floatValue);
			byte[] value = new byte[4]
			{
			bytes[2],
			bytes[3],
			0,
			0
			};
			byte[] value2 = new byte[4]
			{
			bytes[0],
			bytes[1],
			0,
			0
			};
			return new int[2]
			{
			BitConverter.ToInt32(value2, 0),
			BitConverter.ToInt32(value, 0)
			};
		}

		public static int[] ConvertFloatToRegisters(float floatValue, RegisterOrder registerOrder)
		{
			int[] array = ConvertFloatToRegisters(floatValue);
			int[] result = array;
			if (registerOrder == RegisterOrder.HighLow)
			{
				result = new int[2]
				{
				array[1],
				array[0]
				};
			}
			return result;
		}

		public static int[] ConvertIntToRegisters(int intValue)
		{
			byte[] bytes = BitConverter.GetBytes(intValue);
			byte[] value = new byte[4]
			{
			bytes[2],
			bytes[3],
			0,
			0
			};
			byte[] value2 = new byte[4]
			{
			bytes[0],
			bytes[1],
			0,
			0
			};
			return new int[2]
			{
			BitConverter.ToInt32(value2, 0),
			BitConverter.ToInt32(value, 0)
			};
		}

		public static int[] ConvertIntToRegisters(int intValue, RegisterOrder registerOrder)
		{
			int[] array = ConvertIntToRegisters(intValue);
			int[] result = array;
			if (registerOrder == RegisterOrder.HighLow)
			{
				result = new int[2]
				{
				array[1],
				array[0]
				};
			}
			return result;
		}

		public static int[] ConvertLongToRegisters(long longValue)
		{
			byte[] bytes = BitConverter.GetBytes(longValue);
			byte[] value = new byte[4]
			{
			bytes[6],
			bytes[7],
			0,
			0
			};
			byte[] value2 = new byte[4]
			{
			bytes[4],
			bytes[5],
			0,
			0
			};
			byte[] value3 = new byte[4]
			{
			bytes[2],
			bytes[3],
			0,
			0
			};
			byte[] value4 = new byte[4]
			{
			bytes[0],
			bytes[1],
			0,
			0
			};
			return new int[4]
			{
			BitConverter.ToInt32(value4, 0),
			BitConverter.ToInt32(value3, 0),
			BitConverter.ToInt32(value2, 0),
			BitConverter.ToInt32(value, 0)
			};
		}

		public static int[] ConvertLongToRegisters(long longValue, RegisterOrder registerOrder)
		{
			int[] array = ConvertLongToRegisters(longValue);
			int[] result = array;
			if (registerOrder == RegisterOrder.HighLow)
			{
				result = new int[4]
				{
				array[3],
				array[2],
				array[1],
				array[0]
				};
			}
			return result;
		}

		public static int[] ConvertDoubleToRegisters(double doubleValue)
		{
			byte[] bytes = BitConverter.GetBytes(doubleValue);
			byte[] value = new byte[4]
			{
			bytes[6],
			bytes[7],
			0,
			0
			};
			byte[] value2 = new byte[4]
			{
			bytes[4],
			bytes[5],
			0,
			0
			};
			byte[] value3 = new byte[4]
			{
			bytes[2],
			bytes[3],
			0,
			0
			};
			byte[] value4 = new byte[4]
			{
			bytes[0],
			bytes[1],
			0,
			0
			};
			return new int[4]
			{
			BitConverter.ToInt32(value4, 0),
			BitConverter.ToInt32(value3, 0),
			BitConverter.ToInt32(value2, 0),
			BitConverter.ToInt32(value, 0)
			};
		}

		public static int[] ConvertDoubleToRegisters(double doubleValue, RegisterOrder registerOrder)
		{
			int[] array = ConvertDoubleToRegisters(doubleValue);
			int[] result = array;
			if (registerOrder == RegisterOrder.HighLow)
			{
				result = new int[4]
				{
				array[3],
				array[2],
				array[1],
				array[0]
				};
			}
			return result;
		}

		public static string ConvertRegistersToString(int[] registers, int offset, int stringLength)
		{
			byte[] array = new byte[stringLength];
			byte[] array2 = new byte[2];
			checked
			{
				for (int i = 0; i < unchecked(stringLength / 2); i++)
				{
					array2 = BitConverter.GetBytes(registers[offset + i]);
					array[i * 2] = array2[0];
					array[i * 2 + 1] = array2[1];
				}
				return Encoding.Default.GetString(array);
			}
		}

		public static int[] ConvertStringToRegisters(string stringToConvert)
		{
			byte[] bytes = Encoding.ASCII.GetBytes(stringToConvert);
			checked
			{
				int[] array = new int[unchecked(stringToConvert.Length / 2) + unchecked(stringToConvert.Length % 2)];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = bytes[i * 2];
					if (i * 2 + 1 < bytes.Length)
					{
						array[i] |= bytes[i * 2 + 1] << 8;
					}
				}
				return array;
			}
		}
	}
}
