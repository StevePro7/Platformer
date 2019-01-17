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


17/01/2019
Player
x = 0
y = 1
GetBounds()
rectangle = {X:0 Y:32 Width:40 Height:32}

GetBottomCenter()
bottom = {X:20 Y:64}
start    = {X:20 Y:64}
position = {X:20 Y:64}

Player
LoadContent()
FrameWidth = 64
// Calculate bounds within texture size.            
int width = (int)(idleAnimation.FrameWidth * 0.4);		25
int left = (idleAnimation.FrameWidth - width) / 2;		19
int height = (int)(idleAnimation.FrameWidth * 0.8);		51
int top = idleAnimation.FrameHeight - height;			13

localBounds = new Rectangle(left, top, width, height);
localBounds = new Rectangle(19, 13, 25, 51)
localBounds = {X:19 Y:13 Width:25 Height:51}

Position	= {X:20 Y:64}
Origin		= {32, 64}
BoundingRectangle
int left = (int)Math.Round(Position.X - sprite.Origin.X) + localBounds.X;
int left = (int)Math.Round(20 - 32) + 19
		 = 7

int top = (int)Math.Round(Position.Y - sprite.Origin.Y) + localBounds.Y;
int top = (int)Math.Round(64 - 64) + 13
		= 13

BoundingRectangle = new Rectangle(left, top, localBounds.Width, localBounds.Height);
BoundingRectangle = new Rectangle(7, 13, 25, 51)
NOTE:
left + width = 32	(7 + 25)
top + height = 64	(13 + 51)


Collision
Rectangle bounds = BoundingRectangle;
Rectangle(7, 13, 25, 51)
Tile.Width	40
Tile.Height	32
int leftTile = (int)Math.Floor((float)bounds.Left / Tile.Width);
int leftTile = 7 / 40							= 0
int rightTile = (int)Math.Ceiling(((float)bounds.Right / Tile.Width)) - 1;
int rightTile = (32 (7 + 25) / 40)	= 1 - 1  	= 0
int topTile = (int)Math.Floor((float)bounds.Top / Tile.Height);
int topTile = 13 / 32 							= 0
int bottomTile = (int)Math.Ceiling(((float)bounds.Bottom / Tile.Height)) - 1;
int bottomTile = (64 / 32) = 2 - 1				= 1

for (int y = 0; y <= 1; ++y)
	for (int x = 0; x <= 0; ++x)