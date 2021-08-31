using N8Engine.Mathematics;
using N8Engine.Rendering;

namespace N8Engine.Tilemaps
{
    public sealed class AutoTilemap<T> where T : TilePalette, new()
    {
        public static AutoTilemap<T> Generator => new();

        private AutoTilemap() { }

        public AutoTilemap<T> PlaceChunk(Vector position, IntegerVector sizeInTiles, TilePivot pivot)
        {
            if (sizeInTiles.X <= 0 || sizeInTiles.Y <= 0) return this;

            var tilePalette = new T();
            var tileSize = new IntegerVector(tilePalette.TileSize.X * Renderer.NUMBER_OF_CHARACTERS_PER_PIXEL, tilePalette.TileSize.Y);
            var chunkInformation = new ChunkInformation(position, sizeInTiles, tileSize, pivot);

            for (var y = 0; y < sizeInTiles.Y; y++)
                for (var x = 0; x < sizeInTiles.X; x++)
                {
                    var localPosition = new IntegerVector(x, y);
                    GameObject.Create<Tile>().Initialize(localPosition, tilePalette, chunkInformation);
                }
            tilePalette.BaseTilemapObject.Initialize(chunkInformation);
            return this;
        }
    }
}