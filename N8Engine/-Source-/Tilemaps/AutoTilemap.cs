using System.Collections.Generic;
using N8Engine.Mathematics;
using N8Engine.Rendering;

namespace N8Engine.Tilemaps
{
    public sealed class AutoTilemap<T> where T : TilePalette, new()
    {
        private enum TileType
        {
            Left, Right, Top, Bottom, TopRight, TopLeft, BottomRight, BottomLeft, Middle
        }

        public static AutoTilemap<T> Generator => new();

        private AutoTilemap() { }

        public AutoTilemap<T> Place(Vector position, Vector sizeOfChunkInNumberOfTiles, TilePivot pivot)
        {
            if (sizeOfChunkInNumberOfTiles.X <= 0 || sizeOfChunkInNumberOfTiles.Y <= 0) return this;

            var tilePalette = new T();
            var actualTileSize = new Vector(tilePalette.TileSize.X * Window.RATIO_OF_HORIZONTAL_PIXELS_TO_VERTICAL_PIXELS, tilePalette.TileSize.Y);
            var tiles = new List<GameObject>();
            
            for (var y = 0; y < (int) sizeOfChunkInNumberOfTiles.Y; y++)
            {
                for (var x = 0; x < (int) sizeOfChunkInNumberOfTiles.X; x++)
                {
                    var tileType = GetTileType(new Vector(x, y), sizeOfChunkInNumberOfTiles);
                    var sprite = tileType switch
                    {
                        TileType.Left => tilePalette.Left,
                        TileType.Right => tilePalette.Right,
                        TileType.Top => tilePalette.Top,
                        TileType.Bottom => tilePalette.Bottom,
                        TileType.TopLeft => tilePalette.TopLeft,
                        TileType.TopRight => tilePalette.TopRight,
                        TileType.BottomLeft => tilePalette.BottomLeft,
                        TileType.BottomRight => tilePalette.BottomRight,
                        TileType.Middle => tilePalette.Middle,
                        var _ => tilePalette.Middle
                    };
                    var gameObject = GameObject.Create<EmptyGameObject>();
                    gameObject.SpriteRenderer.Sprite = sprite;
                    gameObject.SpriteRenderer.SortingOrder = tilePalette.SortingOrder;
                    gameObject.Transform.Position = position + new Vector(x, y) * actualTileSize;
                    tiles.Add(gameObject);
                }
            }
            var chunkSize = sizeOfChunkInNumberOfTiles * actualTileSize;
            foreach (var tile in tiles)
                tile.Transform.Position = GetTilePositionFromPivot(pivot, tile.Transform.Position, chunkSize, actualTileSize);

            var tilemapCollider = tilePalette.BaseTilemapObject;
            tilemapCollider.Transform.Position = GetColliderPositionFromPivot(position, pivot, chunkSize);
            tilemapCollider.Collider.Size = sizeOfChunkInNumberOfTiles * tilePalette.TileSize;

            return this;
        }

        private TileType GetTileType(Vector tilePosition, Vector numberOfTiles)
        {
            if (numberOfTiles.X == 1)
            {
                if (tilePosition.Y == 0) return TileType.Bottom;
                if (tilePosition.Y == numberOfTiles.Y - 1) return TileType.Top;                
                return TileType.Middle;
            }
            if (tilePosition.X == 0)
            {
                if (tilePosition.Y == 0) return TileType.BottomLeft;
                if (tilePosition.Y == numberOfTiles.Y - 1) return TileType.TopLeft;
                return TileType.Left;
            }
            if (tilePosition.X == numberOfTiles.X - 1)
            {
                if (tilePosition.Y == 0) return TileType.BottomRight;
                if (tilePosition.Y == numberOfTiles.Y - 1) return TileType.TopRight;
                return TileType.Right;
            }
            if (tilePosition.Y == 0)
                return TileType.Bottom;
            if (tilePosition.Y == numberOfTiles.Y - 1)
                return TileType.Top;
            return TileType.Middle;
        }

        private Vector GetTilePositionFromPivot(TilePivot pivot, Vector position, Vector chunkSize, Vector actualTileSize)
        {
            var center = position - chunkSize / 2f;
            var moveLeft = new Vector(-chunkSize.X, 0) / 2f;
            var moveRight = new Vector(chunkSize.X, 0) / 2f;
            var moveUp = new Vector(0, chunkSize.Y) / 2f;
            var moveDown = new Vector(0, -chunkSize.Y) / 2f;
            
            // [=] is the center.
            // X's are the pivot.
            // Slashes are the tilemap.
            var newPosition = pivot switch
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
            newPosition += Vector.Right * actualTileSize / 2f;
            return newPosition;
        }

        private Vector GetColliderPositionFromPivot(Vector position, TilePivot pivot, Vector chunkSize)
        {
            var positionWithCenterPivot = pivot switch
            {
                TilePivot.Center => position,
                TilePivot.Top => position + new Vector(0f, chunkSize.Y / 2f),
                TilePivot.Bottom => position + new Vector(0f, -chunkSize.Y / 2f),
                TilePivot.Right => position + new Vector(-chunkSize.X / 2f, 0f),
                TilePivot.Left => position + new Vector(chunkSize.X / 2f, 0f),
                TilePivot.TopRight => position + new Vector(-chunkSize.X, chunkSize.Y) / 2f,
                TilePivot.TopLeft => position + new Vector(chunkSize.X, chunkSize.Y) / 2f,
                TilePivot.BottomRight => position + new Vector(-chunkSize.X, -chunkSize.Y) / 2f,
                TilePivot.BottomLeft => position + new Vector(chunkSize.X, -chunkSize.Y) / 2f,
                var _ => position
            };
            return positionWithCenterPivot;
        }
    }
}