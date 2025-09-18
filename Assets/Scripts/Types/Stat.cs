using UnityEngine;

public struct Stat
{
    public float baseValue;

    public readonly float Value => baseValue;

    public Stat(float baseValue) => this.baseValue = baseValue;

    public static implicit operator Stat(float value) => new(value);
}