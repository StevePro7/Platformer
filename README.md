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

move + idle
if there is block to immed. left then cannot walk i.e. idle and reverse
if there is no block underneath to immed. left or right then idle + rev

PLAYER
bool
IsAlive
IsOnGround
isJumping
wasJumping


Jump
velocityY = JumpLaunchVelocity * (1.0f - (float)Math.Pow(jumpTime / MaxJumpTime, JumpControlPower));
velocityY = -3500.0f * (1.0f - (float)Math.Pow(0.02 / 0.35, 0.14));
velocityY = -3500.0f * (1.0f - 0.6698455)
velocityY = -3500.0f * 0.330154479
velocityY = -1155.5406765


Documentation
https://www.gamasutra.com/blogs/YoannPignole/20140103/207987/Platformer_controls_how_to_avoid_limpness_and_rigidity_feelings.php
https://medium.com/@btco_code/writing-a-platformer-for-the-tic-80-virtual-console-6fa737abe476