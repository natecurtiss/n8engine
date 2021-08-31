using N8Engine.Mathematics;

namespace N8Engine.Tilemaps
{
    internal sealed class Tile : GameObject
    {
        public void Initialize(IntegerVector localPosition, ChunkInformation chunkInformation)
        {
            var tileType = chunkInformation.TileTypeOf(localPosition);
            var tilePalette = chunkInformation.TilePalette;
            SpriteRenderer.Sprite = tilePalette.SpriteBasedOnTileType(tileType);
            Transform.Position = chunkInformation.TilePositionBasedOnPivot(localPosition);
        }
    }
}