# Platformer
Placeholder repo for Platformer


Install-Package Ninject -Version 3.2.2.0
packages\Ninject.3.2.2.0\lib\net40\Ninject.dll

Install-Package NUnit -Version 3.2.1
packages\NUnit.3.2.1\lib\net40\nunit.framework.dll


Level
Tilemap

TileCollision
:	Passable		not used	can pass thru and not land on	BlockB0-1
.	Passable		used		can pass thru and not land on	<empty>
~	Platform		not used	can pass thru but land on OK	BlockB0-1
-	Platform		used		can pass thru but land on OK	Platform
#	Impassable		used		cannot pass thru but land on	BlockA0-7

1				Player
A,B,C,D			Enemys
X				Exit
G				Gem

50fps
Byte framesPerSecond = Manager.ConfigManager.GlobalConfigData.FramesPerSecond;
Engine.Game.IsFixedTimeStep = Constants.IsFixedTimeStep;
Engine.Game.TargetElapsedTime = TimeSpan.FromSeconds(1.0f / framesPerSecond);
			
gameTime.ElapsedGameTime.Milliseconds		20
gameTime.ElapsedGameTime.TotalSeconds		0.02


MATHS
Abs
Pow
Min
Max
Round
Floor
Ceiling
Sin			not using


ENEMY
64x64
Left	21px
Width	22px
Top		20px		head

localBounds			rectangle around enemy	[less head]
