using System;
using System.IO;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Interfaces;

namespace WindowsGame.Common.Implementation
{
	public class ProdFileProxy : IFileProxy
	{
		public Stream GetStream(String path)
		{
			return TitleContainer.OpenStream(path);
		}
	}
}