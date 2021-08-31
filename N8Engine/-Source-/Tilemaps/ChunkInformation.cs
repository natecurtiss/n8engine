using N8Engine.Mathematics;

namespace N8Engine.Tilemaps
{
    internal readonly struct ChunkInformation
    {
        public readonly Vector Position;
        private readonly IntegerVector _sizeOfEachTile;
        private readonly TilePivot _pivot;
        private readonly IntegerVector _sizeInTiles;

        public IntegerVector TotalSize => _sizeInTiles * _sizeOfEachTile;
        public Vector HalfOfATileToTheRight => Vector.Right * (Vector) _sizeOfEachTile / 2f;

        public ChunkInformation(Vector position, IntegerVector sizeInTiles, IntegerVector sizeOfEachTile, TilePivot pivot)
        {
            Position = position;
            _sizeInTiles = sizeInTiles;
            _sizeOfEachTile = sizeOfEachTile;
            _pivot = pivot;
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
            var tilePosition = (Vector) (localPosition * _sizeOfEachTile) + Position;
            return TilePositionBasedOnPivot(tilePosition);
        }

        public Vector TilePositionBasedOnPivot(Vector tilePosition)
        {
            var chunkSize = (Vector) TotalSize;
            var center = tilePosition - chunkSize / 2f;
            var moveLeft = new Vector(-chunkSize.X, 0) / 2f;
            var moveRight = new Vector(chunkSize.X, 0) / 2f;
            var moveUp = new Vector(0, chunkSize.Y) / 2f;
            var moveDown = new Vector(0, -chunkSize.Y) / 2f;

            // [=] is the center.
            // X's are the pivot.
            // Slashes are the tilemap.
            var newPosition = _pivot switch
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