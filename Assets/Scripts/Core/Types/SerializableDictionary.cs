using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SerializableDictionary<TKey, TValue> : ISerializationCallbackReceiver
{
    [SerializeField] private List<TKey> keys = new();
    [SerializeField] private List<TValue> values = new();

    private Dictionary<TKey, TValue> dict = new();

    public TValue this[TKey key]
    {
        get => dict.TryGetValue(key, out var value) ? value : default;
        set => dict[key] = value;
    }

    public Dictionary<TKey, TValue> ToDictionary() => dict;

    public void OnBeforeSerialize()
    {
        keys.Clear();
        values.Clear();
        foreach (var kvp in dict)
        {
            keys.Add(kvp.Key);
            values.Add(kvp.Value);
        }
    }

    public void OnAfterDeserialize()
    {
        dict = new Dictionary<TKey, TValue>();
        for (int i = 0; i < Math.Min(keys.Count, values.Count); i++)
        {
            dict[keys[i]] = values[i];
        }
    }

    public static implicit operator Dictionary<TKey, TValue>(SerializableDictionary<TKey, TValue> serializableDict) => serializableDict.dict;
}
