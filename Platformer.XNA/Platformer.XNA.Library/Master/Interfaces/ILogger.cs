﻿using System;

namespace WindowsGame.Common.Interfaces
{
	public interface ILogger
	{
		void Initialize();
		void Debug(String message);
		void Error(String message);
		void Fatal(String message);
		void Info(String message);
		void Warn(String message);
	}
}