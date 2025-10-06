using System;
using UnityEngine;

[Serializable]
public class StatModifier
{
    [field: SerializeField] public string Source { get; private set; }
    [field: SerializeField] public float Value { get; private set; }
    [field: SerializeField] public StatModifierType Type { get; private set; }

    public StatModifier(string source, float value, StatModifierType type)
    {
        Source = source;
        Value = value;
        Type = type;
    }

    public static StatModifier Create(string source, float value, StatModifierType type) => new(source, value, type);
}