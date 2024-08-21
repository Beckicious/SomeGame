public static class HexMetrics
{
    public const float OuterToInner = 0.866025404f;
    public const float InnerToOuter = 1f / OuterToInner;

    public const float OuterRadius = 0.5f;
    public const float InnerRadius = OuterRadius * OuterToInner;

    public const float Gap = 0.13f;
    public const float HorizontalDistance = OuterRadius * 1.5f + Gap * OuterToInner;
    public const float VerticalDistance = InnerRadius * 2 + Gap;
}
