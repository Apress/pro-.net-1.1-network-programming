using System;
using System.Net;

namespace Apress.Networking.TCP.FtpUtil
{
	/// <summary>
	/// Summary description for FtpWebRequest.
	/// </summary>
	public class FtpWebRequest : WebRequest
	{
		private string username = "anonymous";
		internal string password = "someuser@somemail.com";
		private bool binaryMode = true;
		private Uri uri;
		private string method = "GET";

		internal FtpWebRequest(Uri uri)
		{
			this.uri = uri;
		}

		public string Username
		{
			get
			{
				return username;
			}
			set
			{
				username = value;
			}
		}
		public string Password
		{
			set
			{
				password = value;
			}
		}

		public bool BinaryMode
		{
			get
			{
				return binaryMode;
			}
			set
			{
				binaryMode = value;
			}
		}

		public override System.Net.WebResponse GetResponse()
		{
			FtpWebResponse response = new FtpWebResponse(this);

			return response;
		}

		public override System.Uri RequestUri
		{
			get
			{
				return uri;
			}
		}

		public void Close()
		{
			
			
		}

		public override string Method
		{
			get
			{
				return method;
			}
			set
			{
				method = value;
			}
		}

	}
}
