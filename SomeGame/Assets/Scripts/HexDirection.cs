public enum HexDirection
{
    N, NE, SE, S, SW, NW
}

public static class HexDirectionExtensions
{
    public static HexDirection Opposite(this HexDirection direction)
    {
        return (int)direction < 3 ? (direction + 3) : (direction - 3);
    }

    public static HexDirection Next(this HexDirection direction)
    {
        return (HexDirection)(((int)direction + 1) % 6);
    }

    public static HexDirection Add(this HexDirection direction, HexDirection other)
    {
        return (HexDirection)(((int)direction + (int)other) % 6);
    }
}
