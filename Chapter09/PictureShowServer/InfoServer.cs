using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Apress.Networking.Multicast
{
	/// <summary>
	/// The InfoServer class 
	/// </summary>
	public class InfoServer
	{
		private IPAddress groupAddress;
		private int groupPort;
		private UnicodeEncoding encoding = new UnicodeEncoding();

		/// <summary>
		/// Construct a InfoServer object.
		/// </summary>
		/// <param name="groupAddress"></param>
		/// <param name="groupPort"></param>
		public InfoServer(IPAddress groupAddress, int groupPort)
		{
			this.groupAddress = groupAddress;
			this.groupPort = groupPort;
		}

		public void Start()
		{
         // Create a new listener thread
			Thread infoThread = new Thread(new ThreadStart(InfoMain));
			infoThread.IsBackground = true;
			infoThread.Start();		
		}

		/// <summary>
		/// Main method of the info thread
		/// </summary>
		protected void InfoMain()
		{
			string configuration = groupAddress.ToString() + ":" + groupPort.ToString();

         // Create a TCP streaming socket that listens to client requests
			Socket infoSocket = new Socket(AddressFamily.InterNetwork,
				SocketType.Stream, ProtocolType.Tcp);

			try
			{
				infoSocket.Bind(new IPEndPoint(IPAddress.Any, 8777));
				infoSocket.Listen(5);

				while (true)
				{
					// Send multicast configuration information to clients
					Socket clientConnection = infoSocket.Accept();
					clientConnection.Send(encoding.GetBytes(configuration));
					clientConnection.Shutdown(SocketShutdown.Both);
					clientConnection.Close();
				}
			}
			finally
			{
				infoSocket.Shutdown(SocketShutdown.Both);
				infoSocket.Close();
			}
		}
	}
}
