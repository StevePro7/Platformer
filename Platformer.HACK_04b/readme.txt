Platformer.HACK_04b

Clone of Platformer.HACK_04
32x32

MAIN
HandleCollisions()
use byte array 32x elements wide for left, right, top, bottom
calc index from posX + posY to lookup index in arrays
use these values for nested for-loop instead of
Math.Floor()
Math.Ceiling()


Add gridlines
Tiles become 32x32 for tiles and change Player to be 32x64
Retain original movement left / right / jump / fall
BUT implement tile collision look ups from int array
Compare against actual original calculations;
They all work fine except when player is near the edges - this needs to be tweaked not to check Tile collision