using N8Engine.Mathematics;
using static N8Engine.Mathematics.Pivot;

namespace N8Engine.Tilemaps
{
    public sealed class AutoTilemap<TPalette, TBaseObject> where TPalette : TilePalette, new() where TBaseObject : GameObject, new()
    {
        private readonly TPalette _palette;
        private Vector _position;
        private IntegerVector _sizeInTiles;
        private IntegerVector _totalSize;
        
        public AutoTilemap() => _palette = new TPalette();

        public AutoTilemap<TPalette, TBaseObject> Place(Vector position, IntegerVector sizeInTiles, Pivot pivot)
        {
            var bottomLeft = position;
            _sizeInTiles = sizeInTiles;
            _totalSize = _sizeInTiles * _palette.TileSize;
            _position = bottomLeft.AdjustedToPivot(BottomLeft, _totalSize, pivot);
            GenerateTiles();
            CreateCollider();
            return this;
        }

        private void GenerateTiles()
        {
            for (var row = 0; row < _sizeInTiles.Y; row++)
            {
                for (var tile = 0; tile < _sizeInTiles.X; tile++)
                {
                    var tileObject = GameObject.Create<Tile>();
                    var localPosition = new IntegerVector(tile, row);
                    tileObject.Transform.Position = _position;
                    tileObject.Transform.Position += localPosition * _palette.TileSize;
                    tileObject.SpriteRenderer.Sprite = _palette.GetSpriteInChunk(localPosition, _sizeInTiles);
                    Debug.Log(tileObject.SpriteRenderer.Sprite + " hello");
                    tileObject.SpriteRenderer.SortingOrder = _palette.SortingOrder;
                }
            }
        }

        private void CreateCollider()
        {
            var tilemapBase = GameObject.Create<TBaseObject>();
            var halfOfTheChunk = (Vector) _totalSize / 2f;
            var center = _position + halfOfTheChunk - _palette.HalfATile;
            tilemapBase.Transform.Position = center;
            tilemapBase.Collider.Size = _totalSize;
        }
    }
}