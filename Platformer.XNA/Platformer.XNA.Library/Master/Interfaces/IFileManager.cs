using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace WindowsGame.Common.Interfaces
{
	public interface IFileManager
	{
		IList<String> LoadTxt(String file);
		IList<T> LoadTxt<T>(String file);
		T LoadXml<T>(String file);
		XElement LoadXElement(String file);
	}
}