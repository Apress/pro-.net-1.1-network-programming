using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Text;

namespace Apress.Networking.TCP.FtpUtil
{
	internal sealed class FtpClient	
	{
		private const int bufferSize = 65536;

		private NetworkStream controlStream;
		private NetworkStream dataStream;
		private TcpClient client;

		private string username;
		private string password;

		private TcpClient dataClient = null;

		public FtpClient(string username, string password)
		{
			this.username = username;
			this.password = password;
		}

		public int GetResponse(out string responseMessage)
		{
			// Read the respose from the server, trim any nulls, and return it.
			byte[] response = new byte[client.ReceiveBufferSize];

			controlStream.Read(response, 0, response.Length);
			responseMessage = Encoding.ASCII.GetString(response).Replace("\0", "");

			return int.Parse(responseMessage.Substring(0, 3));
		}

		public void Connect(string hostname)
		{
			// Set the private fields representing the TCP control connection to the server
			// and the NetworkStream used to communicate with the server
			client = new TcpClient(hostname, 21);
			controlStream = client.GetStream();

			string responseMessage;
			if (GetResponse(out responseMessage) != 220)
			{
				throw new WebException(responseMessage);
			}

			Logon(username, password);
		}

		private void Logon(string username, string password)
		{
			// Send a USER FTP command. The server should respond with a 331 message
			// to ask for the user's password.
			string respMessage;
			int resp = SendCommand("USER " + username, out respMessage);

			if (resp != 331 && resp != 230)
				throw new UnauthorizedAccessException("Unable to login to the FTP server");

			if (resp != 230)
			{
				// Send a PASS FTP command. The server should respond with a 230 message
				// to say that the user is now logged in.
				resp = SendCommand("PASS " + password, out respMessage);
				if (resp != 230)
					throw new UnauthorizedAccessException("FTP server can't authenticate username");
			}
		}

		public NetworkStream GetReadStream(string filename, bool binaryMode)
		{
			return DownloadFile(filename, binaryMode);
		}

		public NetworkStream GetWriteStream(string filename, bool binaryMode)
		{
			return UploadFile(filename, binaryMode);
		}

		internal int SendCommand(string command, out string respMessage)
		{
			// Convert the command string (terminated with a CRLF) into a byte array,
			// and write it to the control stream.
			byte[] request = Encoding.ASCII.GetBytes(command + "\r\n");
			controlStream.Write(request, 0, request.Length);

			return GetResponse(out respMessage);
		}

		private NetworkStream DownloadFile(string filename, bool binaryMode)
		{
			if (dataClient == null)
				dataClient = CreateDataSocket();

			SetBinaryMode(binaryMode);

			string respMessage;
			int resp = SendCommand("RETR " + filename, out respMessage);

			if(resp != 150 && resp != 125) 
			{
				throw new WebException(respMessage);
			}

			dataStream = dataClient.GetStream();

			return dataStream;
		}

		private void SetBinaryMode(bool binaryMode) 
		{
			int resp;
			string respMessage;
			if(binaryMode) 
			{
				resp = SendCommand("TYPE I", out respMessage);
			}
			else 
			{
				resp = SendCommand("TYPE A", out respMessage);
			}
			if (resp != 200) 
			{
				throw new WebException(respMessage);
			}
		}

		private TcpClient CreateDataSocket()
		{
			// request server to listen on a data port (not the default data port)
			// and wait for a connection
			string respMessage;
			int resp = SendCommand("PASV", out respMessage);
			if (resp != 227)
				throw new WebException(respMessage);

			// the response includes the host address and port number
			// IP address and port number separated with ','
			// Creaete the IP address and port number
			int[] parts = new int[6];
			try
			{
				int index1 = respMessage.IndexOf('(');
				int index2 = respMessage.IndexOf(')');
				string endPointData = respMessage.Substring(index1 + 1, index2 - index1 - 1);
				string[] endPointParts = endPointData.Split(',');
				for (int i=0; i < 6; i++)
				{
					parts[i] = int.Parse(endPointParts[i]);
				}
			}
			catch
			{
				throw new WebException("Malformed PASV reply: " + respMessage);
			}

			string ipAddress = parts[0] + "." + parts[1] + "." + parts[2] + "." + parts[3];
			int port = (parts[4] << 8) + parts[5];

			// Create a client socket
			TcpClient dataClient = new TcpClient();

			// connect to the data port of the server
			try 
			{
				IPEndPoint remoteEP = new IPEndPoint(IPAddress.Parse(ipAddress), port);
				dataClient.Connect(remoteEP);
			} 
			catch(Exception) 
			{
				throw new WebException("Can't connect to remote server");
			}
			return dataClient;
		}

		public void Close()
		{
			if (dataStream != null)
			{
				dataStream.Close();
				dataStream = null;
			}

			string respMessage;
			GetResponse(out respMessage);

			Logoff();

			// Close the control TcpClient and NetworkStream
			controlStream.Close();
			client.Close();
		}

		public void Logoff()
		{
			// Send the QUIT command to log off from the server
			string respMessage;
			SendCommand("STAT", out respMessage); // Test only
			GetResponse(out respMessage);		  // STAT has 2 response lines!

			SendCommand("QUIT", out respMessage);
		}

		private NetworkStream UploadFile(string filename, bool binaryMode)
		{
			if (dataClient == null)
				dataClient = this.CreateDataSocket();

			// Set binary or ASCII mode
			SetBinaryMode(binaryMode);

			// Send a STOR command to say we want to upload a file.
			string respMessage;
			int resp = SendCommand("STOR " + filename, out respMessage);
         
			// We should get a 150 response to say that the server is opening the data connection.
			if (resp != 150 && resp != 125)
				throw new WebException("Cannot upload files to the server");

			dataStream = dataClient.GetStream();
			return dataStream;
		}

	}
}
