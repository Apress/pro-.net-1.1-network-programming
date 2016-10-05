using System;

namespace RawSocket
{
	/// <summary>
	/// Summary description for SNMPTest.
	/// </summary>
	public class SNMPTest
	{
		public SNMPTest()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		public static void Main(string[] argv)
		{
			if(argv.Length<2) return;
			string strInfo = string.Empty ;
			int iCommLength;
			int iMIBLength;
			int iDataType;
			int iDataLength;
			int iStartData;
			byte[] byteResponse = new byte[1024];
			SNMP objSNMP = new SNMP();
			Console.WriteLine("SNMP information:");
			// Setting the parameter for GetRequest method
			// and getting the byteResponse
			byteResponse = objSNMP.SNMPGet
			("GetRequest", argv[0], argv[1],
			"1.3.6.1.2.1.1.5.0");
			if (byteResponse[0] == 0xff)
			{
				Console.WriteLine("No Response Received from {0}", argv[0]);
				return;
			}
			// Get the community name and MIB lengths
			// from the packet
			iCommLength = Convert.ToInt16(byteResponse[6]);
			iMIBLength = Convert.ToInt16
			(byteResponse[23 + iCommLength]);
			// Decode the MIB data from the SNMP byteResponse
			iDataType = Convert.ToInt16(byteResponse
			[24 + iCommLength + iMIBLength]);
			iDataLength = Convert.ToInt16(byteResponse
			[25 + iCommLength + iMIBLength]);
			iStartData = 26 + iCommLength + iMIBLength;
			strinfo = Encoding.ASCII.GetString
			(byteResponse, iStartData, iDataLength);
			Console.WriteLine(" sysName - iDataType:{0}, Value: {1}",	iDataType, strInfo);
		}
	}
}
