# Platformer
12/02/2019
Player localBounds width, left, height, top used for Player BoundingRectangle X, Y, Width, Height
and believe never changes - just used to offset BoundingRectangle for Sprite Origin

localBounds
width	25
left	19	X
height	51
top		13	Y

11/02/2019
TODO
Compare that Math.Floor() and Math.Ceiling() will give same answers on SMS
Replace GetIntersectionDepth() extension method to determine overlap rects

Player boundingRectangle
left = 7
topX = 51 = 64 - 13		should be 64-16 = 48 OR 64-8=56 OR 64-12-52 [32-6=26]
wide = 25
bott = 64
How is 7 + 25 calc'd?
Ans 7+25 = 32 which should be 8+24
Remember that tile is 40px wide so
40 = 4 + 4 + 24 + 4 + 4		sprite coll 24px wide
but must add 4px either side to add to 32 plus 4px either side to total 40px tile wide

My guy is 16x32 and tile is 16px wide so my calculation is
16 = 2 + 12 + 2
no px either side for tile as tile is same width as sprite and coll is 12px wide w/ 2px either side
thus my Player boundingRectangle would be
left = 2	(8-4)/2
topX = 26	52/2
wide = 12	24/2
bott = 32	64/2

Also as player does not change size then the boundingRectangle wide + high will NOT change
Therefore, as player moves and changes int(x) and int(y) then move boundingRect left + top by the same int(delta)

In fact, ensure the left=7 and high=51 [above] becomes 8 and 52 so half wide/high also ints
GetIntersectionDepth()


08/02/2019
Finally calc'd the velX and velY for 50fps and 60fps and averaged the amounts for med + sml
Next, would need to replace the calculations with the array index to update position

06/02/2019
Platformer jump logic:
if not pressing jump button then isJumping continues to remain false and jumpTime = 0.0f
as soon as press jump button then isJumping is true and end of frame wasJumping equals isJumping

if press and hold jump then execute jump logic for the duration of the jumpTime [0.35f]
at jumpTime=0.35f then reach apex of the jump and begin falling due to influence of gravity

if while still falling and still holding jump button then do not jump because IsOnGround=False
if after falling and and after hit the ground still holding jump button then do not jump because isJump=wasJump=True
i.e. MUST release jump button and press jump button again before can jump again

SUMMARY
Apply gravity force to calc VelY
Clamp VelY at 550
DoJump() which overrides existing VelY due to jumping [while isJumping=True]
When hit apex of jump then jumpTime=0

until release jump button and press again because 
if still hold jump button after [reach apex 
05/02/2019
Position = {X:60 Y:64}

float velY = velocity.Y + GravityAcceleration*elapsed;
float velY = 0.0f + 3400.0f * 0.02f
int32 velY = 0 + 3400 * 20 / 1000
float velY = 68.0f

Position += velocity * elapsed;
Position += (0, 68) * 0.02
Position.X += 0
Position.Y += 68 * 20 / 1000
Position = {X:60 Y:65}
		
VelY		Vel*0.02	PosY		deltaY
68	68	1	1.36	64	65.36	65	1
136	136	2	2.72	65	67.72	68	3
204	204	3	4.08	68	72.08	72	4
272	272	4	5.44	72	77.44	77	5
340	340	5	6.8		77	83.8	84	7
408	408	6	8.16	84	92.16	92	8
476	476	7	9.52	92	101.52	102	10
544	544	8	10.88	102	112.88	113	11
612	550	9	11		113	124		124	11
618	550	10	11		124	135		135	11
618	550	10	11		135	146		146	11

Don't forget to halve the deltaY for 256*192 as half screen size
							

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