using UnityEngine;

public class HitIndicator : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;

    public void Setup(Color hitColor)
    {
        spriteRenderer.color = hitColor;
    }
}
