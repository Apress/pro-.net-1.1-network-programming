using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Security.Cryptography;
using System.IO;

namespace UDP.Secure.RSA.ChatApp
{
	class Chat
	{
		private static IPAddress remoteIPAddress;
		private static int remotePort;
		private static int localPort;
		private static UTF8Encoding objUtf8;
		private static string strPubKey = "<RSAKeyValue><Modulus>sttDL3xug/BqMk13d6G5vWekmyul/d3pz/Lpvk2Q1GNBSriatLxCRJSuOAie8g/yby624K85qJLwMMzwCru7b+kNTA2dYaK4Nk+FkZMLCVmomiW1zns2KsT1aF9hwr32Nyje3OuJDlHqBtcOpCGbo+kJ+JC88BM1J9AkdoAa+SE=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
		private static string strPriKey = "<RSAKeyValue><Modulus>sttDL3xug/BqMk13d6G5vWekmyul/d3pz/Lpvk2Q1GNBSriatLxCRJSuOAie8g/yby624K85qJLwMMzwCru7b+kNTA2dYaK4Nk+FkZMLCVmomiW1zns2KsT1aF9hwr32Nyje3OuJDlHqBtcOpCGbo+kJ+JC88BM1J9AkdoAa+SE=</Modulus><Exponent>AQAB</Exponent><P>3BoisxTvnh8Xtg/02fTGtr/k8OXUOiEfKwAKzWje36v8zkTfIc4EzdZbRskJywq1NMo9U1EHM3DUv+Ya/KGPzQ==</P><Q>0AcCph/CdQeB2/M+q3BSlzimr9Chw9zaHk1x8MBCHdRB9c26VcS0AmKW+G4VzjWJjI6cK8j/GQjhnRn7UbBypQ==</Q><DP>bikCjwD+gPRs6KmJ0gCp6FOY4V0WYFWthNcLkQ1Y5zfsWsyrpP649tC/dGkwZpggY6CJGwcmBIAHa1hez2yJTQ==</DP><DQ>Uzva1Xkzpvuf+89xrcq9YQArwYDqmKGPLDy0cC2cxq6czarI+XRAyguEeFYjp2RIatLMrcA4QV4KV3+DzQWaeQ==</DQ><InverseQ>eriVG9Kp3CQ/J9PpfMlemC7tPIs6m//LyhKD7J5zLGIzz+71C5QjVi2dRwtvjGJaexOTi+TRIv2fT/LhWmsCDQ==</InverseQ><D>sjfHZ47OtIuf1gXY8AznfnLC05eXrDIuo/YBsY2qredFDQaLqWIZiiq4ur7kWoFHakAbHCGeC3p2+bmLyrYr2nm8Ogj0c1NUneE8ASoKWfnbcWxW377Oeogj16frPUoAgwU1gFURdTxozgNLThVtNItrc3Doa5eJ+U7pRSz2edE=</D></RSAKeyValue>";

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
		static byte[] EncryptData(string strDataGram)
		{
			CspParameters objCspParam = new CspParameters();
			objCspParam.Flags = CspProviderFlags.UseMachineKeyStore;
			objCspParam.KeyContainerName = "ApressRSAStore";

			// Initializing the RSA Cryptography Service Provider (CSP)
			RSACryptoServiceProvider objRSAProvider = new RSACryptoServiceProvider(objCspParam); 

			//Set the Load the public key
			objRSAProvider.FromXmlString(strPubKey);
		
			// Convert string to byte array				
			byte[] bClearText = objUtf8.GetBytes(strDataGram);

			// Encrypting the data recived
			byte[] bytes = objRSAProvider.Encrypt(bClearText, false);
			objRSAProvider.Clear();
			
			return bytes;
		}
		
		static string DecryptData(byte[] bCipherText)
		{
			// Initializing the RSA Cryptography Service Provider (CSP)
			RSACryptoServiceProvider objRSAProvider = new RSACryptoServiceProvider();

			//Set the Load the private key
			objRSAProvider.FromXmlString(strPriKey);

			// Encrypting the data recived
			string strRtnData = objUtf8.GetString(objRSAProvider.Decrypt(bCipherText, false));
			objRSAProvider.Clear();

			return strRtnData;
		}
	}
}
