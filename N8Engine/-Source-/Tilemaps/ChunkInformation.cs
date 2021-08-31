using N8Engine.Mathematics;

namespace N8Engine.Tilemaps
{
    internal readonly struct ChunkInformation
    {
        public readonly Vector Position;
        public readonly IntegerVector SizeOfEachTile;
        public readonly TilePivot Pivot;
        public readonly TilePalette TilePalette;
        
        private readonly IntegerVector _sizeInTiles;

        public IntegerVector TotalSize => _sizeInTiles * SizeOfEachTile;
        private Vector HalfOfATileToTheRight => Vector.Right * (Vector) SizeOfEachTile / 2f;

        public ChunkInformation(Vector position, IntegerVector sizeInTiles, IntegerVector sizeOfEachTile, TilePivot pivot, TilePalette tilePalette)
        {
            Position = position;
            _sizeInTiles = sizeInTiles;
            SizeOfEachTile = sizeOfEachTile;
            Pivot = pivot;
            TilePalette = tilePalette;
        }

        public TileType TileTypeOf(IntegerVector localPosition)
        {
            const int bottom = 0;
            const int left = 0;
            var top = _sizeInTiles.Y - 1;
            var right = _sizeInTiles.X - 1;
            var isSingularColumnOfTiles = _sizeInTiles.X == 1;

            if (isSingularColumnOfTiles)
            {
                if (localPosition.Y == bottom) return TileType.Bottom;
                if (localPosition.Y == top) return TileType.Top;                
                return TileType.Middle;
            }
            if (localPosition.X == left)
            {
                if (localPosition.Y == bottom) return TileType.BottomLeft;
                if (localPosition.Y == top) return TileType.TopLeft;
                return TileType.Left;
            }
            if (localPosition.X == right)
            {
                if (localPosition.Y == bottom) return TileType.BottomRight;
                if (localPosition.Y == top) return TileType.TopRight;
                return TileType.Right;
            }
            if (localPosition.Y == bottom)
                return TileType.Bottom;
            if (localPosition.Y == top)
                return TileType.Top;
            
            return TileType.Middle;
        }

        public Vector TilePositionBasedOnPivot(IntegerVector localPosition)
        {
            var tilePosition = (Vector) (localPosition * SizeOfEachTile) + Position;  
            var chunkSize = (Vector) TotalSize;
            var center = tilePosition - chunkSize / 2f;
            var moveLeft = new Vector(-chunkSize.X, 0) / 2f;
            var moveRight = new Vector(chunkSize.X, 0) / 2f;
            var moveUp = new Vector(0, chunkSize.Y) / 2f;
            var moveDown = new Vector(0, -chunkSize.Y) / 2f;

            // [=] is the center.
            // X's are the pivot.
            // Slashes are the tilemap.
            var newPosition = Pivot switch
            {
                TilePivot.Center => center,
//                           /////////////////   
//                           /////////////////   
//                           //////XXXX///////  
//                           //////XXXX/////// 
//                           /////////////////   
//                           /////////////////

                TilePivot.Top => center + moveUp,
//                                [=][=]
//                                [=][=] 
//                           //////XXXX///////   
//                           //////XXXX///////   
//                           /////////////////  
//                           ///////////////// 
//                           /////////////////   
//                           /////////////////

                TilePivot.Bottom => center + moveDown,
//                           /////////////////   
//                           /////////////////   
//                           /////////////////  
//                           ///////////////// 
//                           ///////XXXX//////   
//                           ///////XXXX//////
//                                 [=][=]
//                                 [=][=]                

                TilePivot.Right => center + moveLeft,
//              /////////////////   
//              /////////////////   
//              //////////////XXX [=][=] 
//              //////////////XXX [=][=]
//              /////////////////   
//              /////////////////

                TilePivot.Left => center + moveRight,
//                                      /////////////////
//                                      /////////////////
//                               [=][=] XXX////////////// 
//                               [=][=] XXX////////////// 
//                                      /////////////////
//                                      /////////////////

                TilePivot.TopRight => center + moveLeft + moveDown,
//                               [=][=]
//                               [=][=]
//              //////////////XXX
//              //////////////XXX
//              /////////////////
//              /////////////////
//              /////////////////
//              /////////////////         

                TilePivot.TopLeft => center + moveRight + moveDown,
//                               [=][=]
//                               [=][=]
//                                     XXX//////////////
//                                     XXX//////////////
//                                     /////////////////
//                                     /////////////////
//                                     /////////////////
//                                     /////////////////

                TilePivot.BottomRight => center + moveLeft + moveUp,
//              /////////////////
//              /////////////////
//              /////////////////
//              /////////////////
//              //////////////XXX
//              //////////////XXX
//                               [=][=]
//                               [=][=]

                TilePivot.BottomLeft => center + moveRight + moveUp,
//                                     /////////////////
//                                     /////////////////
//                                     /////////////////
//                                     /////////////////
//                                     XXX//////////////
//                                     XXX//////////////
//                               [=][=]
//                               [=][=]

                var _ => center
            };
            newPosition += HalfOfATileToTheRight;
            return newPosition;
        }
    }
}