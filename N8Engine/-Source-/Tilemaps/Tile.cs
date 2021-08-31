using N8Engine.Mathematics;

namespace N8Engine.Tilemaps
{
    internal sealed class Tile : GameObject
    {
        public void Initialize(IntegerVector localPosition, TilePalette tilePalette, ChunkInformation chunkInformation)
        {
            var tileType = chunkInformation.TileTypeOf(localPosition);
            SpriteRenderer.Sprite = tilePalette.SpriteBasedOnTileType(tileType);
            SpriteRenderer.SortingOrder = tilePalette.SortingOrder;
            Transform.Position = chunkInformation.TilePositionBasedOnPivot(localPosition);
        }
    }
}