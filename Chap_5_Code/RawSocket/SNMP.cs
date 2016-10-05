using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace RawSocket
{
	/// <summary>
	/// Summary description for SNMP.
	/// </summary>
	public class SNMP
	{
		public SNMP()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		public byte[] getRequest(string Request, string Host, string Community, string  MIBData)
		{
			int iSnmpLength;
			int iCommunityLength = Community.Length;
			int ibyteMIBLength = byteMIBvals.Length;
			int iCount = 0;
			int iTemp;
			int i;
			int iInitialbyteMIBLength = ibyteMIBLength;
			int iPosition = 0;
			string[] byteMIBvals = MIBData.Split('.');
			byte[] bytePacket = new byte[1024];
			byte[] byteMIB = new byte[1024];
			// Convert the string byteMIB into a byte array of integer values
			for (i = 0; i < iInitialbyteMIBLength; i++)
			{
				iTemp = Convert.ToInt16(byteMIBvals[i]);
				if (iTemp > 127)
				{
					byteMIB[iCount] = Convert.ToByte(128 + (iTemp / 128));
					byteMIB[iCount + 1] = Convert.ToByte(iTemp - ((iTemp / 128) * 128));
						iCount += 2;
					ibyteMIBLength++;
				}
				else
				{
					byteMIB[iCount] = Convert.ToByte(iTemp);
					iCount++;
				}
			}
			// Length of entire SNMP bytePacket
			iSnmpLength = 29 + iCommunityLength + ibyteMIBLength - 1;
			// The SNMP sequence start
			bytePacket[iPosition++] = 0x30; //Sequence start
			bytePacket[iPosition++] = Convert.ToByte(iSnmpLength - 2);
			// Sequence size
			// SNMP version
			bytePacket[iPosition++] = 0x02; // Integer type
			bytePacket[iPosition++] = 0x01; // Length
			bytePacket[iPosition++] = 0x00; // SNMP version 1
			// Community name
			bytePacket[iPosition++] = 0x04; // String type
			bytePacket[iPosition++] = Convert.ToByte(iCommunityLength);
			// Length
			// Convert community name to byte array
			byte[] data = Encoding.ASCII.GetBytes(Community);
			for (i = 0; i < data.Length; i++)
			{
				bytePacket[iPosition++] = data[i];
			}
			// Add GetRequest or GetNextRequest value
			if (Request == "GetRequest")
				bytePacket[iPosition++] = 0xA0;
			else
				bytePacket[iPosition++] = 0xA1;
			bytePacket[iPosition++] = Convert.ToByte(20 + ibyteMIBLength - 1);
			// Size of total byteMIB
			//Request ID
			bytePacket[iPosition++] = 0x02; // Integer type
			bytePacket[iPosition++] = 0x04;
			bytePacket[iPosition++] = 0x00; // SNMP Request ID
			bytePacket[iPosition++] = 0x00;
			bytePacket[iPosition++] = 0x00;
			bytePacket[iPosition++] = 0x01;
			// Error status
			bytePacket[iPosition++] = 0x02; //Integer type
			bytePacket[iPosition++] = 0x01; bytePacket[iPosition++] = 0x00;
			// SNMP error status
			// Error index
			bytePacket[iPosition++] = 0x02; // Integer type
			bytePacket[iPosition++] = 0x01; // Length
			bytePacket[iPosition++] = 0x00; // SNMP error index
			// Start of variable bindings
			bytePacket[iPosition++] = 0x30;
			bytePacket[iPosition++] = Convert.ToByte(6 + ibyteMIBLength - 1);
			// Size of variable binding
			bytePacket[iPosition++] = 0x30; bytePacket[iPosition++] = Convert.ToByte(6 +
												ibyteMIBLength - 1 - 2); // Size
			bytePacket[iPosition++] = 0x06; // Object type
			bytePacket[iPosition++] = Convert.ToByte(ibyteMIBLength - 1);
			// Start of byteMIB
			bytePacket[iPosition++] = 0x2b;
			// Place byteMIB array in bytePacket
			for(i = 2; i < ibyteMIBLength; i++)
			bytePacket[iPosition++] = Convert.ToByte(byteMIB[i]);
			bytePacket[iPosition++] = 0x05; //Null object value
			bytePacket[iPosition++] = 0x00; //Null

			// Send bytePacket to destination
			Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram,
			ProtocolType.Udp);
			sock.SetSocketOption(SocketOptionLevel.Socket,
			SocketOptionName.ReceiveTimeout, 5000);
			IPHostEntry ihe = Dns.Resolve(Host);
			IPEndPoint iep = new IPEndPoint(ihe.AddressList[0], 161);
			EndPoint ep = (EndPoint)iep;
			sock.SendTo(bytePacket, iSnmpLength, SocketFlags.None, iep);
			// Receive response from bytePacket
			try
			{
				int recv = sock.ReceiveFrom(bytePacket, ref ep);
			}
			catch (SocketException)
			{
				bytePacket[0] = 0xff;
			}
			return bytePacket;
		}
	}
}
