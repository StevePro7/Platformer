﻿#if WINDOWS
using log4net;
using log4net.Config;
using WindowsGame.Common.Interfaces;

namespace WindowsGame.Common.Implementation
{
	public class ProdLogger : ILogger
	{
		private static readonly ILog Log = LogManager.GetLogger(typeof(ProdLogger));

		public void Initialize()
		{
			XmlConfigurator.Configure();
		}

		public void Debug(string message)
		{
			Log.Debug(message);
		}
		public void Error(string message)
		{
			Log.Error(message);
		}
		public void Fatal(string message)
		{
			Log.Fatal(message);
		}
		public void Info(string message)
		{
			Log.Info(message);
		}
		public void Warn(string message)
		{
			Log.Warn(message);
		}

	}
}
#endif