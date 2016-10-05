using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Security.Cryptography;
using System.IO;

namespace UDP.Secure.Rijndael.ChatApp
{
	class Chat
	{
private static IPAddress remoteIPAddress;
private static int remotePort;
private static int localPort;
private static UTF8Encoding objUtf8;
private static string strCryptoKey = "!i~6ox1i@]t2K'y$";
private static string strCryptoVI =  "!~x7Oq{6+q1@#VI$";

		[STAThread]
		static void Main(string[] args)
		{
			try
			{
				// Get necessary data for connection
				Console.WriteLine("Enter Local Port");
				localPort = Convert.ToInt16(Console.ReadLine());

				Console.WriteLine("Enter Remote Port");         
				remotePort = Convert.ToInt16(Console.ReadLine());         

				Console.WriteLine("Enter Remote IP address");
				remoteIPAddress = IPAddress.Parse(Console.ReadLine());

				objUtf8 = new UTF8Encoding();

				// Create thread for listening 
				Thread tRec = new Thread(new ThreadStart(Receiver));
				tRec.Start();
      
				while(true)
				{
					Send(Console.ReadLine());
				}      
			}
			catch (Exception e)
			{
				Console.WriteLine(e.ToString()); 
			}
		}

		private static void Send(string datagram) 
		{
			// Create UdpClient
			UdpClient sender = new UdpClient();

			// Create IPEndPoint with details of remote host
			IPEndPoint endPoint = new IPEndPoint(remoteIPAddress, remotePort);      

			try 
			{			
				// Convert string to byte array				
				//byte[] bClearText = objUtf8.GetBytes(datagram);

				// Encrypting the data recived
				//byte[] bytes = objRSAProvider.Encrypt(bClearText, false);

				byte[] bytes = EncryptData(datagram);

				// Send data
				sender.Send(bytes, bytes.Length, endPoint);            
				//sender.Send(bClearText, bClearText.Length, endPoint);            
			} 
			catch (Exception e) 
			{
				Console.WriteLine(e.ToString());
			}
			finally 
			{
				// Close connection
				sender.Close();
			}   
		}

		public static void Receiver()
		{
			// Create a UdpClient for reading incoming data.
			UdpClient receivingUdpClient = new UdpClient(localPort);

			// IPEndPoint with remote host information
			IPEndPoint RemoteIpEndPoint = null; 

			try
			{
				Console.WriteLine(
					"-----------*******Ready for chat!!!*******-----------");

				while(true)
				{
					// Wait for datagram
					byte[] receiveBytes = receivingUdpClient.Receive(ref RemoteIpEndPoint); 
					
					//Decrypt the incomming byte arrary
					//byte[] bClearText = objRSAProvider.Decrypt(receiveBytes, false);

					// Convert and display data
					//string returnData = objUtf8.GetString(bClearText);
					string returnData = DecryptData(receiveBytes);
					
					//string returnData = objUtf8.GetString(receiveBytes);
					Console.WriteLine("-" + returnData.ToString());                                 
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.ToString ()); 
			}
		}

		/// <summary>
		/// The EncryptData method will encrypt the given file using the DES algorithm
		/// </summary>
		/// <param name="strDataGram"></param>
		/// <returns>byte[]</returns>
		static byte[] EncryptData(string strDataGram)
		{
			byte[] bCipherText = null;
			try
			{			
				//Create the Rijandael Service Provider object and assign the key and vector to it
				RijndaelManaged RijndaelProvider = new RijndaelManaged();
				RijndaelProvider.Key = objUtf8.GetBytes(strCryptoKey);
				RijndaelProvider.IV = objUtf8.GetBytes(strCryptoVI);

				ICryptoTransform RijndaelEncrypt = RijndaelProvider.CreateEncryptor();

				// Convert string to byte array				
				byte[] bClearText = objUtf8.GetBytes(strDataGram);
				MemoryStream objMs = new MemoryStream();

				//Create Crypto Stream that transforms a stream using the encryption
				CryptoStream objCs = new CryptoStream(objMs, RijndaelEncrypt, CryptoStreamMode.Write);

				//Write out encrypted content into MemoryStream
				objCs.Write(bClearText, 0, bClearText.Length);
				objCs.FlushFinalBlock();

				//Get the output
				bCipherText = objMs.ToArray();

				//Close the stream handlers
				objCs.Close();
				objMs.Close();
			}
			catch (Exception e)
			{
				Console.WriteLine(e.ToString ()); 
			}
			return bCipherText;
		}
		
		/// <summary>
		/// The DecryptData method will decrypt the given file using the DES algorithm
		/// </summary>
		/// <param name="bCipherText"></param>
		/// <returns>string</returns>
		static string DecryptData(byte[] bCipherText)
		{
			string sEncoded ="";

			try
			{
				//Create the RijndaelManaged Service Provider object and assign the key and vector to it
				RijndaelManaged RijndaelProvider = new RijndaelManaged();
				RijndaelProvider.Key = objUtf8.GetBytes(strCryptoKey);
				RijndaelProvider.IV = objUtf8.GetBytes(strCryptoVI);
				
				ICryptoTransform RijndaelDecrypt= RijndaelProvider.CreateDecryptor();

				//Create a MemoryStream with the input
				MemoryStream objMs = new MemoryStream(bCipherText, 0, bCipherText.Length);

				//Create Crypto Stream that transforms a stream using the decryption
				CryptoStream objCs = new CryptoStream(objMs, RijndaelDecrypt, CryptoStreamMode.Read);
			
				// read out the result from the Crypto Stream
				StreamReader objSr = new StreamReader(objCs);
				sEncoded = objSr.ReadToEnd();

				objSr.Close();
				objCs.Close();
				objMs.Close();
			}
			catch (Exception e)
			{
				Console.WriteLine(e.ToString ()); 
			}

			return sEncoded;
		}
	}
}
