using System;
using System.Net;

namespace Apress.Networking.TCP.FtpUtil
{
	/// <summary>
	/// Summary description for FtpWebRequestCreator.
	/// </summary>
	public class FtpRequestCreator : IWebRequestCreate
	{
		public FtpRequestCreator()
		{

		}

		#region Implementation of IWebRequestCreate
		public System.Net.WebRequest Create(System.Uri uri)
		{
			return new FtpWebRequest(uri);
		}
		#endregion
	}
}
