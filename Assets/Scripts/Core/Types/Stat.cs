using System.Collections.Generic;

public class Stat
{
    public float BaseValue { get; private set; }

    private readonly List<float> flatModifiers = new();
    private readonly List<float> percentModifiers = new();

    public float Value
    {
        get
        {
            float finalValue = BaseValue;

            for (int i = 0; i < flatModifiers.Count; i++)
                finalValue += flatModifiers[i];

            for (int i = 0; i < percentModifiers.Count; i++)
                finalValue *= 1 + percentModifiers[i];

            return finalValue;
        }
    }

    public Stat(float baseValue) => BaseValue = baseValue;

    public void SetBaseValue(float value) => BaseValue = value;

    public void AddFlatModifier(float value) => flatModifiers.Add(value);
    public void RemoveFlatModifier(float value) => flatModifiers.Remove(value);

    public void AddPercentModifier(float value) => percentModifiers.Add(value);
    public void RemovePercentModifier(float value) => percentModifiers.Remove(value);

    public void ClearModifiers()
    {
        flatModifiers.Clear();
        percentModifiers.Clear();
    }

    public static implicit operator float(Stat stat) => stat.Value;
    public static implicit operator Stat(float baseValue) => new(baseValue);
}
