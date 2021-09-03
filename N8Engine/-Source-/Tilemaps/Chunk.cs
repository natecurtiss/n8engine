using System.Collections.Generic;
using N8Engine.Mathematics;

namespace N8Engine.Tilemaps
{
    internal readonly struct Chunk
    {
        // Sort of a magic number - but at least it works.
        private Vector Center => _position + new Vector(HalfOfTheChunk.X - _palette.HalfATile.X, 0f);
        private Vector ColliderSize => _palette.TileSize * _sizeInTiles;
        private Vector HalfOfTheChunk => Size / 2f;
        private Vector Size => _palette.ActualTileSize * _sizeInTiles;

        private readonly Vector _position;
        private readonly IntegerVector _sizeInTiles;
        private readonly TilePalette _palette;
        
        public Chunk(Vector position, IntegerVector sizeInTiles, TilePalette palette)
        {
            _position = position;
            _sizeInTiles = sizeInTiles;
            _palette = palette;
        }

        public void GenerateTiles<TTile>() where TTile : GameObject, new()
        {
            for (var row = 0; row < _sizeInTiles.Y; row++)
            {
                for (var tile = 0; tile < _sizeInTiles.X; tile++)
                {
                    var tileObject = GameObject.Create<TTile>();
                    var localPosition = new IntegerVector(tile, row);
                    tileObject.Transform.Position = _position;
                    tileObject.Transform.Position += localPosition * _palette.ActualTileSize;
                    tileObject.SpriteRenderer.Sprite = _palette.GetSpriteInChunk(localPosition, _sizeInTiles);
                    tileObject.SpriteRenderer.SortingOrder = _palette.SortingOrder;
                }
            }
        }

        public void GenerateTiles() => GenerateTiles<Tile>();

        public TilemapBase CreateCollider()
        {
            var tilemapBase = _palette.BaseObject;
            tilemapBase.Transform.Position = Center;
            tilemapBase.Collider.Size = ColliderSize;
            return tilemapBase;
        }
    }
}