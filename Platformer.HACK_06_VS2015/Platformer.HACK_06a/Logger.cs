using System;
using log4net;
using log4net.Config;

namespace Platformer
{
	public static class Logger
	{
		private static readonly ILog Log = LogManager.GetLogger(typeof(Logger));

		public static void Initialize()
		{
			XmlConfigurator.Configure();
		}

		public static void Debug(string message)
		{
			Log.Debug(message);
		}
		public static void Error(string message)
		{
			Log.Error(message);
		}
		public static void Fatal(string message)
		{
			Log.Fatal(message);
		}
		public static void Info(string message)
		{
			Log.Info(message);
		}
		public static void Warn(string message)
		{
			Log.Warn(message);
		}
	}
}
