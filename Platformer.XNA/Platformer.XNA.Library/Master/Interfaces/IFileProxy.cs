using System;
using System.IO;

namespace WindowsGame.Common.Interfaces
{
	public interface IFileProxy
	{
		Stream GetStream(String path);
	}
}
