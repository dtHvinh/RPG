using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitVFXPool : MonoBehaviour
{
    [SerializeField] private HitIndicator prefab;
    [SerializeField] private int initialSize = 20;

    private readonly Queue<HitIndicator> pool = new();
    public static HitVFXPool Instance { get; private set; }

    private void Awake()
    {
        Instance = this;

        for (int i = 0; i < initialSize; i++)
        {
            AddToPool();
        }
    }

    private void AddToPool()
    {
        HitIndicator obj = Instantiate(prefab, transform);
        obj.gameObject.SetActive(false);
        pool.Enqueue(obj);
    }

    public void Spawn(Vector3 position, Color color)
    {
        if (pool.Count == 0)
        {
            AddToPool();
        }

        HitIndicator popup = pool.Dequeue();
        popup.transform.position = position;
        popup.gameObject.SetActive(true);
        popup.Setup(color);

        StartCoroutine(ReturnToPool(popup, 1.5f));
    }

    private IEnumerator ReturnToPool(HitIndicator obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (obj != null && obj.gameObject.activeSelf)
        {
            obj.gameObject.SetActive(false);
            pool.Enqueue(obj);
        }
    }
}
