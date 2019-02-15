Platformer.HACK_04b

Clone of Platformer.HACK_04
Add gridlines
Tiles become 32x32 for tiles and change Player to be 32x64
Retain original movement left / right / jump / fall
BUT implement tile collision look ups from int array
Compare against actual original calculations;
They all work fine except when player is near the edges - this needs to be tweaked not to check Tile collision