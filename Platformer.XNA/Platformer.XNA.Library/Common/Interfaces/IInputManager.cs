using System;
using Microsoft.Xna.Framework;

namespace WindowsGame.Common.Interfaces
{
	public interface IInputManager
	{
		void Initialize();
		void Update(GameTime gameTime);

		Boolean Escape();

		void SetMotors(Single leftMotor, Single rightMotor);
		void ResetMotors();
	}
}
