using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Stat
{
    [SerializeField] private float baseValue = 0f;
    [SerializeField] private List<StatModifier> modifiers = new();

    private bool _isDirty = true;
    private float _currentValue = 0f;

    public float BaseValue => baseValue;

    public event EventHandler OnStatModified;

    public float Value
    {
        get
        {
            if (_isDirty)
            {
                _currentValue = StatFormular.FinalStat(BaseValue, modifiers);
                _isDirty = false;
            }
            return (float)Math.Round(_currentValue, 2);
        }
    }

    public Stat(float baseValue) => this.baseValue = baseValue;

    public void AddModifier(StatModifier modifier)
    {
        modifiers.Add(modifier);
        OnModifiedInvoke();
    }

    public void RemoveModifiersFromSource(string source)
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

