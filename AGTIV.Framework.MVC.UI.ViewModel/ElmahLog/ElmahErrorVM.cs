using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;
using System.Xml.Serialization;

namespace AGTIV.Framework.MVC.UI.ViewModel.ElmahLog
{
    public class ElmahErrorVM
    {
        [DisplayName("Error Id")]
        public Guid ErrorId { get; set; }

        public string Application { get; set; }

        public string Host { get; set; }

        public string Type { get; set; }

        public string Source { get; set; }

        public string Message { get; set; }

        public string User { get; set; }

        public int StatusCode { get; set; }

        [DisplayName("Time (UTC)")]
        public DateTime TimeUtc { get; set; }

        public int Sequence { get; set; }

        public string AllXml { get; set; }

		public XMLError Error { get; set; }
    }

	[XmlRoot(ElementName = "value")]
	public class Value
	{
		[XmlAttribute(AttributeName = "string")]
		public string String { get; set; }
	}

	[XmlRoot(ElementName = "item")]
	public class Item
	{
		[XmlElement(ElementName = "value")]
		public Value Value { get; set; }
		[XmlAttribute(AttributeName = "name")]
		public string Name { get; set; }
	}

	[XmlRoot(ElementName = "serverVariables")]
	public class ServerVariables
	{
		[XmlElement(ElementName = "item")]
		public List<Item> Item { get; set; }
	}

	[XmlRoot(ElementName = "form")]
	public class Form
	{
		[XmlElement(ElementName = "item")]
		public List<Item> Item { get; set; }
	}

	[XmlRoot(ElementName = "cookies")]
	public class Cookies
	{
		[XmlElement(ElementName = "item")]
		public List<Item> Item { get; set; }
	}

	[XmlRoot(ElementName = "error")]
	public class XMLError
	{
		[XmlAttribute(AttributeName = "application")]
		public string Application { get; set; }
		[XmlElement(ElementName = "cookies")]
		public Cookies Cookies { get; set; }
		[XmlAttribute(AttributeName = "detail")]
		public string Detail { get; set; }
		[XmlElement(ElementName = "form")]
		public Form Form { get; set; }
		[XmlAttribute(AttributeName = "host")]
		public string Host { get; set; }
		[XmlAttribute(AttributeName = "message")]
		public string Message { get; set; }
		[XmlAttribute(AttributeName = "time")]
		public DateTime Time { get; set; }
		[XmlAttribute(AttributeName = "type")]
		public string Type { get; set; }
		[XmlAttribute(AttributeName = "source")]
		public string Source { get; set; }
		[XmlElement(ElementName = "serverVariables")]
		public ServerVariables ServerVariables { get; set; }

		[ScriptIgnore]
		public string[] DetailSplit
		{
			get {
				var detailsplit = new string[] { };

				if (!string.IsNullOrEmpty(Detail))
				{
					detailsplit = Detail.Split(new[] { " at " }, StringSplitOptions.None);
				}

				return detailsplit;
			}
		}

	}
}
