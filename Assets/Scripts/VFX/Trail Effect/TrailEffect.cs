using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class TrailEffect : MonoBehaviour
{
    protected SpriteRenderer sr;
    private float lifeTime = 0.5f;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    protected virtual void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0f)
        {
            AfterimageTrailPool.Instance.ReturnToPool(gameObject);
        }
    }

    public void Setup(SpriteRenderer mainRenderer, float duration)
    {
        sr.transform.SetPositionAndRotation(mainRenderer.transform.position, mainRenderer.transform.rotation);
        sr.sprite = mainRenderer.sprite;
        sr.flipX = mainRenderer.flipX;
        sr.sortingLayerID = mainRenderer.sortingLayerID;
        sr.sortingOrder = mainRenderer.sortingOrder - 1;

        lifeTime = duration;
    }
}
