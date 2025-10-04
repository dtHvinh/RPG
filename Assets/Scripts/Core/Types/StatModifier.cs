using System;

public class StatModifier
{
    public Type Source { get; private set; }
    public float Value { get; private set; }
    public StatModifierType Type { get; private set; }

    public StatModifier(Type source, float value, StatModifierType type)
    {
        Source = source;
        Value = value;
        Type = type;
    }

    public static StatModifier Create(Type source, float value, StatModifierType type) => new(source, value, type);
}