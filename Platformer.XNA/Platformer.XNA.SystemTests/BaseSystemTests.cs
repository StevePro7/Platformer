using System;
using WindowsGame.Common.Managers;
using WindowsGame.SystemTests.Master.Implementation;
using NUnit.Framework;
using WindowsGame.Common;
using WindowsGame.Common.Static;
using WindowsGame.Common.TheGame;
using WindowsGame.Master.Interfaces;
using WindowsGame.Master.IoC;

namespace WindowsGame.SystemTests
{
	public abstract class BaseSystemTests
	{
		protected ILevelManager LevelManager;

		// mklink /D C:\Platformer.Content E:\GitHub\StevePro7\Platformer\Platformer.XNA\Platformer.XNA\Platformer.XNA\bin\x86\Debug\
		protected const String CONTENT_ROOT = @"C:\Platformer.Content\";

#pragma warning disable 618
		[TestFixtureSetUp]
#pragma warning restore 618
		public void TestFixtureSetUp()
		{
			Registration.Initialize();
			IoCContainer.Initialize<IFileProxy, TestFileProxy>();

			IGameManager manager = GameFactory.Resolve();
			MyGame.Construct(manager);
		}

#pragma warning disable 618
		[TestFixtureTearDown]
#pragma warning restore 618
		public void TestFixtureTearDown()
		{
			GameFactory.Release();
		}

	}
}
