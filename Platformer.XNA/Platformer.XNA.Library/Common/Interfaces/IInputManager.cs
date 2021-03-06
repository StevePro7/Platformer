﻿using System;
using Microsoft.Xna.Framework;

namespace WindowsGame.Common.Interfaces
{
	public interface IInputManager
	{
		void Initialize();
		void Update(GameTime gameTime);

		Boolean Escape();
		Boolean PlayerUp();
		Boolean PlayerDown();
		Boolean PlayerLeft();
		Boolean PlayerRght();
		Boolean PlayerJump();
		Boolean ScrollLeft();
		Boolean ScrollRght();

		void SetMotors(Single leftMotor, Single rightMotor);
		void ResetMotors();
	}
}
