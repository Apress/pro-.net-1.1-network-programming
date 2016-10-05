using System;
using System.Xml;
using System.Text;

namespace Apress.Networking.Multicast
{
	/// <summary>
	/// Represents a segment of a picture with information about the picture.
	/// </summary>
	public class PicturePackage
	{
		private string name;
		private int id;
		private int segmentNumber;
		private int numberOfSegments;
		private byte[] segmentBuffer;

		public string Name
		{
			get
			{
				return name;
			}
		}

		public int Id
		{
			get
			{
				return id;
			}
		}

		public int SegmentNumber
		{
			get
			{
				return segmentNumber;
			}
		}

		public int NumberOfSegments
		{
			get
			{
				return numberOfSegments;
			}
		}

		public byte[] SegmentBuffer
		{
			get
			{
					return segmentBuffer;
			}
		}

		// Create a picture segment by using the parts of the segment
		// Used from the server application
		public PicturePackage(string name, int id, int segmentNumber, int numberOfSegments, byte[] segmentBuffer)
		{
			this.name = name;
			this.id = id;
			this.segmentNumber = segmentNumber;
			this.numberOfSegments = numberOfSegments;
			this.segmentBuffer = segmentBuffer;
		}

		// Create a picture segment by using XML code
		// Used form the client application
		public PicturePackage(string xml)
		{
			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.LoadXml(xml);
			XmlNode rootNode = xmlDoc.SelectSingleNode("PicturePackage");
			id = int.Parse(rootNode.Attributes["Number"].Value);

			XmlNode nodeName = rootNode.SelectSingleNode("Name");
			this.name = nodeName.InnerXml;

			XmlNode nodeData = rootNode.SelectSingleNode("Data");
			numberOfSegments = int.Parse(nodeData.Attributes["LastSegmentNumber"].Value);
			segmentNumber = int.Parse(nodeData.Attributes["SegmentNumber"].Value);
			int size = int.Parse(nodeData.Attributes["Size"].Value);
			segmentBuffer = Convert.FromBase64String(nodeData.InnerText);
		}


		// Return XML code representing a picture segment
		public string GetXml()	
		{
			XmlDocument doc = new XmlDocument();

			// root element <PicturePackage>
			XmlElement picturePackage = doc.CreateElement("PicturePackage");

			// <PicturePackage Number="number"></PicturePackage>
			XmlAttribute pictureNumber = doc.CreateAttribute("Number");
			pictureNumber.Value = id.ToString();
			picturePackage.Attributes.Append(pictureNumber);

			// <Name>pictureName</Name>
			XmlElement pictureName = doc.CreateElement("Name");
			pictureName.InnerText = name;
			picturePackage.AppendChild(pictureName);			
			
			// <Data SegmentNumber="" Size="">base-64 encoded fragment
			XmlElement data = doc.CreateElement("Data");
			XmlAttribute numberAttr = doc.CreateAttribute("SegmentNumber");
			numberAttr.Value = segmentNumber.ToString();
			data.Attributes.Append(numberAttr);

			XmlAttribute lastNumberAttr = doc.CreateAttribute("LastSegmentNumber");
			lastNumberAttr.Value = numberOfSegments.ToString();
			data.Attributes.Append(lastNumberAttr);

			data.InnerText = Convert.ToBase64String(segmentBuffer);

			XmlAttribute sizeAttr = doc.CreateAttribute("Size");
			sizeAttr.Value = segmentBuffer.Length.ToString();
			data.Attributes.Append(sizeAttr);

			picturePackage.AppendChild(data);

			doc.AppendChild(picturePackage);

			return doc.InnerXml;
		}
	}
}
