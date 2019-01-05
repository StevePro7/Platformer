using System;
using Microsoft.Xna.Framework;

namespace WindowsGame.Common.Interfaces
{
	public interface IInputManager
	{
		void Initialize();
		void LoadContent();
		void Update(GameTime gameTime);

		void SetMotors(Single leftMotor, Single rightMotor);
		void ResetMotors();
	}
}
