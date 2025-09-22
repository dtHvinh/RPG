using UnityEngine;

public static class SpriteRendererExtensions
{
    public static float GetWidth(this SpriteRenderer spriteRenderer)
    {
        return spriteRenderer.bounds.size.x;
    }
}
