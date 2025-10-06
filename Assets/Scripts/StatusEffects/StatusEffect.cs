using System;
using UnityEngine;

[Serializable]
public abstract class StatusEffect : IComparable<StatusEffect>
{
    [SerializeField] private EffectType effectType;
    [SerializeField] private EffectTypeId effectTypeId;
    [SerializeField] private string source;
    [SerializeField] private float duration;
    [SerializeField] private float tickElapse;

    public const float InfiniteDuration = -1;

    public EffectType EffectType => effectType;
    public EffectTypeId EffectTypeId => effectTypeId;
    public string Source => source;

    public float Duration => duration;
    public float TickElapsed => tickElapse;
    public bool IsExpired => !IsInfiniteEffect() && TickElapsed >= Duration;

    protected StatusEffect(string source, float duration, EffectType effectType, EffectTypeId effectTypeId)
    {
        this.source = source;
        this.effectType = effectType;
        this.effectTypeId = effectTypeId;
        this.duration = duration;
    }

    public virtual void OnTick(float delta, bool second)
    {
        if (!IsInfiniteEffect())
        {
            tickElapse += delta;
        }
    }

    public abstract void EffectStart();

    public abstract void EffectEnd();

    public void ResetDuration(float duration)
    {
        this.duration = duration;
        tickElapse = 0;
    }

    public bool IsInfiniteEffect()
    {
        return Duration == InfiniteDuration;
    }

    public void AddDuration(float additionalDuration)
    {
        duration += additionalDuration;
    }

    /// <summary>
    /// Update source and end effect.
    /// </summary>
    public virtual void OverrideEffect(StatusEffect existEffect)
    {
        existEffect.source = Source;
        EffectEnd();
    }

    public abstract int CompareTo(StatusEffect other);
}
