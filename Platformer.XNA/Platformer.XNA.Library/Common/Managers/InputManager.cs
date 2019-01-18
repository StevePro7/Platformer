using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using WindowsGame.Common.Interfaces;
using WindowsGame.Master.Inputs;

namespace WindowsGame.Managers.Inputs
{
	public class InputManager : IInputManager
	{
		private readonly IJoystickInput joystickInput;
		private readonly IMouseScreenInput mouseScreenInput;
		private readonly IKeyboardInput keyboardInput;

		public InputManager(IJoystickInput joystickInput, IKeyboardInput keyboardInput, IMouseScreenInput mouseScreenInput)
		{
			this.joystickInput = joystickInput;
			this.keyboardInput = keyboardInput;
			this.mouseScreenInput = mouseScreenInput;
		}

		public void Initialize()
		{
			joystickInput.Initialize();
			mouseScreenInput.Initialize();
		}

		public void Update(GameTime gameTime)
		{
			joystickInput.Update(gameTime);
			keyboardInput.Update(gameTime);
			mouseScreenInput.Update(gameTime);
		}

		public Boolean Escape()
		{
			return keyboardInput.KeyHold(Keys.Escape);
		}

		public Boolean PlayerUp()
		{
			return keyboardInput.KeyHold(Keys.Up);
		}
		public Boolean PlayerDown()
		{
			return keyboardInput.KeyHold(Keys.Down);
		}

		public Boolean PlayerLeft()
		{
			return keyboardInput.KeyPress(Keys.Left);
		}
		public Boolean PlayerRght()
		{
			return keyboardInput.KeyPress(Keys.Right);
		}
		public Boolean PlayerJump()
		{
			return keyboardInput.KeyPress(Keys.Space);
		}

		public Boolean ScrollLeft()
		{
			return keyboardInput.KeyHold(Keys.I);
		}
		public Boolean ScrollRght()
		{
			return keyboardInput.KeyHold(Keys.J);
		}


		public void SetMotors(Single leftMotor, Single rightMotor)
		{
			joystickInput.SetMotors(leftMotor, rightMotor);
		}

		public void ResetMotors()
		{
			SetMotors(0, 0);
		}

	}
}
