// FlexModbus.Interfaces.IModbusClient
namespace FlexModbus.Interfaces
{
    public interface IModbusClient
    {
		bool[] SendReadStatusCoils(int _addressDevice, int _firstRegister, int _quantityRegisters);

		bool[] SendReadStatusDigitalStatus(int _addressDevice, int _firstRegister, int _quantityRegisters);

		int[] SendReadHoldingRegisters(int _addressDevice, int _firstRegister, int _quantityRegisters);

		int[] SendReadInputRegisters(int _addressDevice, int _firstRegister, int _quantityRegisters);

		bool SendForceSingleCoil(int _addressDevice, int _firstRegister, bool _statusToWrite);

		int SendPresetSingleRegister(int _addressDevice, int _firstRegister, int _valueToWrite);

		bool SendForceMultipleCoils(int _addressDevice, int _firstRegister, bool[] _valueToWrite);

		bool SendPresetMultipleRegisters(int _addressDevice, int _firstRegister, int[] _valuesToWrite);
	}
}
