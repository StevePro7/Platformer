Platformer.HACK_04c

Clone of Platformer.HACK_04b

MAIN
Actually should employ velY + gravity here

Save HandleCollisions() rectangle comparison for 16x16
Test code for HandleCollisions() rectangle comparison


HandleCollisions()
Vector2 depth = RectangleExtensions.GetIntersectionDepth(bounds, tileBounds);


Add gridlines
Tiles become 32x32 for tiles and change Player to be 32x64
Combine velocity arrays and tile collision look up arrays
This would be SMS game implementation but 2x the size
i.e.
32x32 instead of 16x16