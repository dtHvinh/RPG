using System;
using System.Collections.Generic;

public class Stat
{
    private bool _isDirty = true;
    private float _currentValue = 0f;

    public float BaseValue { get; private set; }

    private readonly List<StatModifier> modifiers = new();

    public event EventHandler OnStatModified;

    public float Value
    {
        get
        {
            if (_isDirty)
            {
                float finalValue = BaseValue;
                float percentAdd = 0f;

                foreach (var mod in modifiers)
                {
                    switch (mod.Type)
                    {
                        case StatModifierType.Flat:
                            finalValue += mod.Value;
                            break;
                        case StatModifierType.PercentAdd:
                            percentAdd += mod.Value;
                            break;
                        case StatModifierType.PercentMult:
                            finalValue *= 1 + mod.Value;
                            break;
                    }
                }

                if (percentAdd != 0)
                    finalValue *= 1 + percentAdd;

                _currentValue = finalValue;
                _isDirty = false;
            }
            return _currentValue;
        }
    }

    public Stat(float baseValue) => BaseValue = baseValue;

    public void AddModifier(StatModifier modifier)
    {
        modifiers.Add(modifier);
        OnModifiedInvoke();
    }

    public void RemoveModifiersFromSource(Type source)
    {
        modifiers.RemoveAll(m => m.Source == source);
        OnModifiedInvoke();
    }

    public void ClearModifiers()
    {
        modifiers.Clear();
        OnModifiedInvoke();
    }

    public static implicit operator float(Stat stat) => stat.Value;
    public static implicit operator Stat(float baseValue) => new(baseValue);

    private void OnModifiedInvoke()
    {
        _isDirty = true;
        OnStatModified?.Invoke(this, EventArgs.Empty);
    }
}



