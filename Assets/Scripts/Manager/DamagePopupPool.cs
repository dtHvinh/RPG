using System;
using System.Collections.Generic;
using UnityEngine;

public class DamagePopupPool : MonoBehaviour
{
    [SerializeField] private DamagePopup prefab;
    [SerializeField] private int initialSize = 20;
    [SerializeField] private List<ColorPair> damageTypeColors;

    private readonly Queue<DamagePopup> _pool = new();
    private Dictionary<DameTypes, Color> _color;

    public static DamagePopupPool Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        _color = new Dictionary<DameTypes, Color>();
        foreach (var pair in damageTypeColors)
        {
            _color[pair.dameType] = pair.color;
        }

        for (int i = 0; i < initialSize; i++)
        {
            AddToPool();
        }
    }

    private void AddToPool()
    {
        DamagePopup popup = Instantiate(prefab, transform);
        popup.gameObject.SetActive(false);
        _pool.Enqueue(popup);
    }

    public void Spawn(Vector3 position, int damage, DameTypes dameType, bool isCrit = false)
    {
        if (_pool.Count == 0)
        {
            AddToPool();
        }

        DamagePopup popup = _pool.Dequeue();
        popup.transform.position = position;
        popup.gameObject.SetActive(true);
        popup.Setup(damage, _color[dameType], isCrit);

        StartCoroutine(ReturnToPool(popup, 1.5f));
    }

    private System.Collections.IEnumerator ReturnToPool(DamagePopup popup, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (popup != null && popup.gameObject.activeSelf)
        {
            popup.gameObject.SetActive(false);
            _pool.Enqueue(popup);
        }
    }

    [Serializable]
    struct ColorPair
    {
        public DameTypes dameType;
        public Color color;
    }
}
