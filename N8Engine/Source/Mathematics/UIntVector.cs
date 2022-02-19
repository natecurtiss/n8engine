namespace N8Engine.Mathematics;

// TODO: The rest of this lmao.
public struct UIntVector
{
    readonly uint _x;
    readonly uint _y;
    
    public uint X
    {
        get => _x;
        set => this = new(value, _y);
    }
    
    public uint Y
    {
        get => _y;
        set => this = new(_x, value);
    }

    public UIntVector(uint both)
    {
        _x = both;
        _y = both;
    }

    public UIntVector(uint x, uint y)
    {
        _x = x;
        _y = y;
    }
}