using System;
using System.IO;
using System.Net.Sockets;

namespace Apress.Networking.TCP.FtpUtil
{
	/// <summary>
	/// FtpWebStream to close the underlying FtpWebResponse
	/// </summary>
	internal class FtpWebStream : Stream
	{
		private FtpWebResponse response;
		private NetworkStream dataStream;

		public FtpWebStream(NetworkStream dataStream, FtpWebResponse response)
		{
			this.dataStream = dataStream;
			this.response = response;
		}

		public override void Close()
		{
			response.Close();
			base.Close();	
		}

		public override void Flush()
		{
			dataStream.Flush();		
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			return dataStream.Read(buffer, offset, count);
		}

		public override long Seek(long offset, System.IO.SeekOrigin origin)
		{
			throw new NotSupportedException("Seek not supported");
		}

		public override void SetLength(long value)
		{
			throw new NotSupportedException("SetLength not supported");				
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			dataStream.Write(buffer, offset, count);	
		}

		public override bool CanRead
		{
			get
			{
				return dataStream.CanRead;
			}
		}

		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		public override bool CanWrite
		{
			get
			{
				return dataStream.CanWrite;
			}
		}

		public override long Length
		{
			get
			{
				throw new NotSupportedException("Length not supported");
			}
		}

		public override long Position
		{
			get
			{
				throw new NotSupportedException("Position not supported");
			}
			set
			{
				throw new NotSupportedException("Position not supported");
			}
		}
	}
}
