using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EndlessLoopBackground : MonoBehaviour
{
    private LinkedList<SpriteRenderer> spriteRenderers;
    private Camera mainCamera;

    private void Awake()
    {
        spriteRenderers = spriteRenderers = new LinkedList<SpriteRenderer>(
            GetComponentsInChildren<SpriteRenderer>()
            .OrderBy(e => e.transform.position.x)
        );
        mainCamera = Camera.main;
    }

    private void OnValidate()
    {
        if (spriteRenderers != null && spriteRenderers.Count == 0)
            Debug.LogWarning("Endless background will work properly if there`re atleast 3 sprites");
    }

    private void Update()
    {
        if(ShouldScroll(ScrollDirection.Right))
            Scroll(ScrollDirection.Right);
        else if (ShouldScroll(ScrollDirection.Left))
            Scroll(ScrollDirection.Left);
    }

    private void Scroll(ScrollDirection direction)
    {
        if (direction == ScrollDirection.Right)
        {
            var left = spriteRenderers.First.Value;
            spriteRenderers.RemoveFirst();

            var right = spriteRenderers.Last.Value;
            float newX = right.transform.position.x + right.bounds.size.x;
            left.transform.position = new Vector3(newX, left.transform.position.y);

            spriteRenderers.AddLast(left);
        }
        else if (direction == ScrollDirection.Left)
        {
            var right = spriteRenderers.Last.Value;
            spriteRenderers.RemoveLast();

            var left = spriteRenderers.First.Value;
            float newX = left.transform.position.x - right.bounds.size.x;
            right.transform.position = new Vector3(newX, right.transform.position.y);

            spriteRenderers.AddFirst(right);
        }
    }

    private bool ShouldScroll(ScrollDirection direction)
    {
        float camX = mainCamera.transform.position.x;

        if(direction == ScrollDirection.Left)
        {
            SpriteRenderer left = spriteRenderers.First.Value;

            float scrollThreshold = Mathf.Max(mainCamera.GetWidth().Half(), left.GetWidth().Half());

            if(camX.DistaceTo(left.transform.position.x) <= scrollThreshold)
                return true;
        }
        else if (direction == ScrollDirection.Right)
        {
            var right = spriteRenderers.Last.Value;

            float scrollThreshold = Mathf.Max(mainCamera.GetWidth().Half(), right.GetWidth().Half());

            if (camX.DistaceTo(right.transform.position.x) <= scrollThreshold)
                return true;
        }

        return false;
    }

    private enum ScrollDirection
    {
        Left,
        Right
    }
}
