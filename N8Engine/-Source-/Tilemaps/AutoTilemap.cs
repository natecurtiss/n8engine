using N8Engine.Mathematics;

namespace N8Engine.Tilemaps
{
    public sealed class AutoTilemap<TPalette> where TPalette : TilePalette, new()
    {
        private readonly TPalette _palette;

        public AutoTilemap() => _palette = new TPalette();

        public AutoTilemap<TPalette> Place(Vector position, IntegerVector sizeInTiles, Pivot pivot)
        {
            var bottomLeft = position;
            var totalSize = sizeInTiles * _palette.ActualTileSize;
            var positionAdjustedToPivot = bottomLeft.AdjustToPivot(totalSize, pivot);
            
            var chunk = new Chunk(positionAdjustedToPivot, sizeInTiles, _palette);
            chunk.GenerateTiles();
            chunk.CreateCollider();
            return this;
        }
    }
}