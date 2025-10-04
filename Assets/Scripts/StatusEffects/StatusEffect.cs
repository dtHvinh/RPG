using System;

[Serializable]
public abstract class StatusEffect : IComparable<StatusEffect>
{
    public EffectType EffectType { get; }
    public EffectTypeId EffectTypeId { get; }
    public Type Source { get; private set; }

    public float Duration { get; protected set; }
    public float Elapsed { get; protected set; }
    public bool IsExpired => Elapsed >= Duration;


    protected StatusEffect(Type source, float duration, EffectType effectType, EffectTypeId effectTypeId)
    {
        Source = source;
        EffectType = effectType;
        EffectTypeId = effectTypeId;
        Duration = duration;
    }

    public virtual void Tick()
    {
        Elapsed += 1;
    }

    public abstract void EffectStart();

    public abstract void EffectEnd();

    public void ResetDuration(float duration)
    {
        Duration = duration;
        Elapsed = 0;
    }

    public void AddDuration(float additionalDuration)
    {
        Duration += additionalDuration;
    }

    public virtual void OverrideEffect(StatusEffect existEffect)
    {
        existEffect.Source = Source;
    }

    public abstract int CompareTo(StatusEffect other);
}
