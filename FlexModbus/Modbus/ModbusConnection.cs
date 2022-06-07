using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Threading;
using FlexModbus.Enums;

namespace FlexModbus.Modbus
{
    class ModbusConnection
    {

		private static volatile int TransactionID = 0;

		private static volatile object ReadWriteLock = new object();

		private int PortService { get; set; } = 10000;


		private IPAddress IpAddressDevice { get; set; } = IPAddress.None;


		public int TimeOutConnection { get; set; } = 500;


		public int TimeOutSend { get; set; } = 500;


		public int TimeOutReceive { get; set; } = 500;


		private TcpClient TcpClientService { get; set; } = new TcpClient();


		private NetworkStream NetworkStreamService { get; set; }

		public ModbusConnection(string _ipAddressDevice, int _portService, int _timeOutConnection = 500, int _timeOutSend = 500, int _timeOutReceive = 500)
		{
			var (flag2, ipString) = IsValidIpAddress(_ipAddressDevice);
			try
			{
				if (flag2)
				{
					IpAddressDevice = IPAddress.Parse(ipString);
				}
				else
				{
					IpAddressDevice = Dns.GetHostEntry(_ipAddressDevice).AddressList[0];
				}
				if (_timeOutConnection < 300 || _timeOutConnection > 5000)
				{
					throw new Exception("Timeout value should is between 300 and 5000 ms.");
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
			TimeOutSend = _timeOutSend;
			TimeOutReceive = _timeOutReceive;
			TimeOutConnection = _timeOutConnection;
			PortService = _portService;
		}

		private Tuple<bool, string> IsValidIpAddress(string _ipAddressDevice)
		{
			Match match = Regex.Match(_ipAddressDevice, "^\\d{1,3}.\\d{1,3}.\\d{1,3}.\\d{1,3}$");
			return new Tuple<bool, string>(match.Success, match.Success ? match.Value : "");
		}

		public bool OpenConnection()
		{
			try
			{
				if (TcpClientService.Connected)
				{
					CloseConnection();
				}
				TcpClientService.ExclusiveAddressUse = true;
				TcpClientService.SendBufferSize = 8192;
				TcpClientService.ReceiveBufferSize = 8192;
				TcpClientService.SendTimeout = TimeOutSend;
				TcpClientService.ReceiveTimeout = TimeOutReceive;
				if (TcpClientService.ConnectAsync(IpAddressDevice, PortService).Wait(TimeOutConnection) && TcpClientService.Connected)
				{
					NetworkStreamService = TcpClientService.GetStream();
					return true;
				}
				return false;
			}
			catch (Exception ex)
			{
				Console.WriteLine("Open connection: " + ex.Message);
				return false;
			}
		}

		internal int GetTransactionID()
		{
			return TransactionID;
		}

		public bool StatusConnection()
		{
			if (TcpClientService != null)
			{
				return TcpClientService.Connected;
			}
			return false;
		}

		public void CloseConnection()
		{
			try
			{
				if (TcpClientService != null)
				{
					if (NetworkStreamService != null)
					{
						NetworkStreamService.Close(300);
						NetworkStreamService.Dispose();
					}
					TcpClientService.Close();
					TcpClientService.Dispose();
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Close connection: " + ex.Message);
			}
		}

		internal byte[] SendRequest(byte[] _requestToSend, int _sizeBufferExpected, ETypeReqModbus _typeReqModbus)
		{
			byte[] array = new byte[_sizeBufferExpected];
			lock (ReadWriteLock)
			{
				try
				{
					NetworkStream networkStream2 = (NetworkStreamService = new NetworkStream(TcpClientService.Client));
					using (networkStream2)
					{
						if (NetworkStreamService.CanWrite)
						{
							NetworkStreamService.Write(_requestToSend, 0, _requestToSend.Length);
							NetworkStreamService.Flush();
							if (_typeReqModbus == ETypeReqModbus.TCPIP)
							{
								TransactionID = ((++TransactionID <= 65535) ? TransactionID : 0);
							}
						}
						Thread.Sleep(5);
						if (NetworkStreamService.CanRead && NetworkStreamService.Read(array, 0, _sizeBufferExpected) == _sizeBufferExpected)
						{
							return array;
						}
					}
					return null;
				}
				catch (IOException ex)
				{
					if (ex.InnerException.GetType() == typeof(SocketException))
					{
						SocketException ex2 = (SocketException)ex.InnerException;
						if (ex2.SocketErrorCode == SocketError.NotConnected)
						{
							OpenConnection();
						}
						else if (ex2.SocketErrorCode == SocketError.InProgress)
						{
							Thread.Sleep(100);
						}
					}
					return null;
				}
				catch (Exception ex3)
				{
					Console.WriteLine("Write and read: " + ex3.Message);
					return null;
				}
			}
		}
	}
}
