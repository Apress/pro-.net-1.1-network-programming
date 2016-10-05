using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Apress.Networking.Multicast
{
	public class PicturePackager
	{

		protected PicturePackager()
		{
		}

		// Return picture segments for a complete picture
		public static PicturePackage[] GetPicturePackages(string name, int id, Image picture)
		{
			return GetPicturePackages(name, id, picture, 4000);
		}

		// Return picture segments for a complete picture
		public static PicturePackage[] GetPicturePackages(string name, int id, Image picture, int segmentSize)
		{
			// save the picture in a byte array
			MemoryStream stream = new MemoryStream();
			picture.Save(stream, ImageFormat.Jpeg);

			// Calculate the number of segments to split the picture
			int numberSegments = (int)stream.Position / segmentSize + 1;

			PicturePackage[] packages = new PicturePackage[numberSegments];

			// Create the picture segments
			int sourceIndex = 0;
			for (int i=0; i < numberSegments; i++)
			{
				// Calculate the size of the segment buffer
				int bytesToCopy = (int)stream.Position - sourceIndex;
				if (bytesToCopy > segmentSize)
					bytesToCopy = segmentSize;

				byte[] segmentBuffer = new byte[bytesToCopy];

				Array.Copy(stream.GetBuffer(), sourceIndex, segmentBuffer, 0, bytesToCopy);

				packages[i] = new PicturePackage(name, id, i + 1, numberSegments, segmentBuffer);

				sourceIndex += bytesToCopy;
			}

			return packages;
		}

		// Returns a complete picture by passing all segments
		// of the picture
		public static Image GetPicture(PicturePackage[] packages)
		{
			int fullSizeNeeded = 0;
			int numberPackages = packages[0].NumberOfSegments;
			int pictureId = packages[0].Id;

			// Calculate the size of the picture data
			// and check for consistent picture id's
			for (int i=0; i < numberPackages; i++)
			{
				fullSizeNeeded += packages[i].SegmentBuffer.Length;
				if (packages[i].Id != pictureId)
					throw new ArgumentException("Inconsistent picture ids passed", "packages");
			}

			// Merge the segments to a complete picture binary
			byte[] buffer = new byte[fullSizeNeeded];
			int destinationIndex = 0;
			for (int i=0; i < numberPackages; i++)
			{
				int length = packages[i].SegmentBuffer.Length;
				Array.Copy(packages[i].SegmentBuffer, 0, buffer, destinationIndex, length);
				destinationIndex += length;
			}

			// Create the image from the binary data
			MemoryStream stream = new MemoryStream(buffer);
			Image image = Image.FromStream(stream);

			return image;
		}

	}
}
