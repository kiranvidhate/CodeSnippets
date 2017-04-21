using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using csDBUtils = DBUtils.csDBUtils;
using DBUtils;
using Utilities;
using NetPacket;

namespace netZcom.micros
{
	class MicrosTCPListener
	{
		private Socket m_socListener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
		private readonly object _locker = new object();
		private readonly object _dataLocker = new object();
		//private short NetLinkPort = short.Parse(ConnectionConst.NetLinkPort);
		private AutoResetEvent hSync = new AutoResetEvent(false);
		public AutoResetEvent hExit = new AutoResetEvent(false);

		public class CSocketPacket
		{
			public System.Net.Sockets.Socket thisSocket;
			public byte[] dataBuffer = new byte[short.Parse(ConnectionConst.MaxSocketSize)];
			public AsyncCallback pfnReceiveDataCallBack;
			public ParameterizedThreadStart pfnRequestCallBack; // for delayed requests stored in NetZQueue
			public byte[] ProcessRequestAndGetBytesToSend()
			{
				return new byte[0];
			}
		}

		public MicrosTCPListener(short port)
		{
			try
			{
				// Subscribe to events from base types
				StaticNetlinkData.LogInPacket += INetlinkPacket_LogInPacket;
				StaticNetlinkData.LogOutPacket += INetlinkPacket_LogOutPacket;
				//bind to local IP Address on port=port...
				m_socListener.Bind(new IPEndPoint(IPAddress.Any, port));
				//start listening...
				m_socListener.Listen(4);
				// create the call back for any client connections...
				m_socListener.BeginAccept(new AsyncCallback(OnClientConnect), null);
			}
			catch (ObjectDisposedException ode)
			{
				Globals.log.WriteToLog(ITCLog.LogLevels.Debug, "\nRun: Listener Socket is invalid\n Closing...." + ode.Message);
				m_socListener.Close();
			}
			catch (SocketException se)
			{
				Globals.log.WriteToLog(ITCLog.LogLevels.Debug, "\nRun: cannot activate main listener!\n Closing...." + se.Message);
				m_socListener.Close();
			}
			catch (Exception e)
			{
				Globals.log.WriteToLog(ITCLog.LogLevels.Debug, "\n" + e.Message);
				m_socListener.Close();
			}
		}

		void INetlinkPacket_LogInPacket(object sender, LogEventArgs e)
		{
#if DEBUG
			if(e.LogType == NetPacket.LOG_TYPE.Debug)
				Globals.log.WriteToLog(ITCLog.LogLevels.Debug, "\n" + e.Message);
#endif
		}
		void INetlinkPacket_LogOutPacket(object sender, LogEventArgs e)
		{
			INetlinkPacket_LogInPacket(sender, e);
		}
		~MicrosTCPListener()
		{
			StopListening();
			hExit.Set();
			Thread.Sleep(1000); // let processes/threads urgently exit
		}
		private void StopListening()
		{
			if (m_socListener != null)
				m_socListener.Close();
		}
		public void OnClientConnect(IAsyncResult asyn)
		{
			try
			{
				hSync.Reset();
				Socket socRecipient;
				lock (_locker) // the only place to really synchronize, because of shared m_socListener - when creating a new connection channel
				{
					socRecipient = m_socListener.EndAccept(asyn);
					m_socListener.BeginAccept(new AsyncCallback(OnClientConnect), null);
				}
				WaitForData(socRecipient);
			}
			catch (ObjectDisposedException)
			{
				Globals.log.WriteToLog(ITCLog.LogLevels.Debug, "\n OnClientConnect: Socket has been closed\n");
			}
			catch (SocketException)
			{
				Globals.log.WriteToLog(ITCLog.LogLevels.Debug, "\n OnClientConnect: Broken listener\n");
			}
			finally
			{
				hSync.Set();
			}
		}
		public void WaitForData(Socket socRecipient)
		{
			CSocketPacket theSocPkt = new CSocketPacket();
			try
			{
				theSocPkt.thisSocket = socRecipient;
				theSocPkt.pfnReceiveDataCallBack = new AsyncCallback(OnDataReceived);
				theSocPkt.pfnRequestCallBack = new ParameterizedThreadStart(ProcessRequest);
				// now start to listen for any data...
				socRecipient.BeginReceive(theSocPkt.dataBuffer, 0, theSocPkt.dataBuffer.Length, SocketFlags.None, theSocPkt.pfnReceiveDataCallBack, theSocPkt);
			}
			catch (SocketException)
			{
				if (theSocPkt.thisSocket == null)
					Globals.log.WriteToLog(ITCLog.LogLevels.Debug, "\nWaitForData: NULL Accepting Socket!\n");
				else
					Globals.log.WriteToLog(ITCLog.LogLevels.Debug, "\nWaitForData: Socket [" + theSocPkt.thisSocket.RemoteEndPoint.ToString() + "] is disconnected\n");
			}
		}
		public void ProcessRequest(object theSocObj)
		{
			CSocketPacket theSocPkt = (CSocketPacket)theSocObj;
			if (theSocPkt.thisSocket.Connected)
			{
				theSocPkt.thisSocket.EndReceive(null);
			}

		}
		public void OnDataReceived(IAsyncResult asyn)
		{
			lock (_dataLocker)
			{ 
				CSocketPacket theSockId = (CSocketPacket)asyn.AsyncState;
				try
				{
					if (theSockId.thisSocket == null)
						return;
					int iRx = 0;
					//end receive...
					if (!theSockId.thisSocket.Connected || (iRx = theSockId.thisSocket.EndReceive(asyn)) == 0)
					{
						//Globals.log.WriteToLog(ITCLog.LogLevels.Debug, "Connection to " + theSockId.thisSocket.RemoteEndPoint.ToString() + " closed!");
						theSockId.thisSocket.Close();
						theSockId.thisSocket = null;
						return;
					}

					#region test data
					//  ********* TO TEST THE RECEIVED DATA - UNCOMMENT NEXT BLOCK
					//char[] chars = new char[iRx + 1];
					//System.Text.Decoder d = System.Text.Encoding.UTF8.GetDecoder();
					//int charLen = d.GetChars(theSockId.dataBuffer, 0, iRx, chars, 0);
					//string szData = new System.String(chars);
					//Globals.log.WriteToLog(ITCLog.LogLevels.Debug, "Sender: " + theSockId.thisSocket.RemoteEndPoint.ToString() + " Data: " + szData + "\n");
					#endregion test data
					#region process request
					int count = theSockId.thisSocket.Send(new netZcomRequest(theSockId.dataBuffer).ProcessTCPRequest());
					System.Threading.Thread.Sleep(10);
					if (count < 16)
					{ 
						//Globals.stQueue.Add(theSockId); - to be added later, after timeout
						throw new Exception("Failed to send data for Socket [" + theSockId.thisSocket.RemoteEndPoint.ToString() + "]");
					}
					#endregion process request
					// July 26, 2013 S.M. the next row probably must be above
					WaitForData(theSockId.thisSocket); // just in case all the data comes in more than one shot
				}
				catch (ITCPacketException e)
				{
					Globals.log.WriteToLog(ITCLog.LogLevels.Debug, "\nOnDataReceived: Socket [" + theSockId.thisSocket.RemoteEndPoint.ToString() + "]:\n" + e.Message);
				}
				catch (ObjectDisposedException ode)
				{
					Globals.log.WriteToLog(ITCLog.LogLevels.Debug, "\nOnDataReceived: Socket [" + theSockId.thisSocket.RemoteEndPoint.ToString() + "] has been disposed improperly:\n" + ode.Message);
				}
				catch (SocketException se)
				{
					Globals.log.WriteToLog(ITCLog.LogLevels.Debug, "\nOnDataReceived: " + se.Message);
				}
				catch (Exception e)
				{
					Globals.log.WriteToLog(ITCLog.LogLevels.Debug, "\nOnDataReceived: Socket [" + theSockId.thisSocket.RemoteEndPoint.ToString() + "] Send() failed:\n" + e.Message);
				}
			}
	   }
	}
}
