using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platformer
{
	public class Config
	{
		public Config(bool invincibility, bool optionalBlock)
		{
			Invincibility = invincibility;
			OptionalBlock = optionalBlock;
		}

		public bool Invincibility { get; private set; }
		public bool OptionalBlock { get; private set; }
	}
}
