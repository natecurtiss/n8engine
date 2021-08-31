using N8Engine.Mathematics;
using N8Engine.Rendering;

namespace N8Engine.Tilemaps
{
    public class BaseTilemapObject : GameObject
    {
        internal void Initialize(ChunkInformation chunkInformation)
        {
            var centerOfChunk = chunkInformation.TilePositionBasedOnPivot(chunkInformation.Position);
            var chunkSize = (Vector) chunkInformation.TotalSize;
            Transform.Position = centerOfChunk + new Vector(chunkSize.X / 2f, 0f) - chunkInformation.HalfOfATileToTheRight;
            Collider.Size = new Vector(chunkSize.X / Renderer.NUMBER_OF_CHARACTERS_PER_PIXEL, chunkSize.Y);
        }
    }
}