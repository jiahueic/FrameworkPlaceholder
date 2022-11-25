using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AGTIV.Framework.MVC.Entities.Workflow
{

	[XmlRoot(ElementName = "KeyValueOfstringstring", Namespace = "http://schemas.microsoft.com/2003/10/Serialization/Arrays")]
	public class KeyValueOfstringstring
	{
		[XmlElement(ElementName = "Key", Namespace = "http://schemas.microsoft.com/2003/10/Serialization/Arrays")]
		public string Key { get; set; }
		[XmlElement(ElementName = "Value", Namespace = "http://schemas.microsoft.com/2003/10/Serialization/Arrays")]
		public string Value { get; set; }
	}

	[XmlRoot(ElementName = "ArrayOfKeyValueOfstringstring", Namespace = "http://schemas.microsoft.com/2003/10/Serialization/Arrays")]
	public class ArrayOfKeyValueOfstringstring
	{
		[XmlElement(ElementName = "KeyValueOfstringstring", Namespace = "http://schemas.microsoft.com/2003/10/Serialization/Arrays")]
		public List<KeyValueOfstringstring> KeyValueOfstringstring { get; set; }
		[XmlAttribute(AttributeName = "i", Namespace = "http://www.w3.org/2000/xmlns/")]
		public string I { get; set; }
		[XmlAttribute(AttributeName = "xmlns")]
		public string Xmlns { get; set; }
	}
}
