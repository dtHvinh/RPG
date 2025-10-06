using System;
using UnityEngine;

public class SpeedUpEffect : StatusEffect
{
    private readonly SpeedUpEffectConfig _config;

    public SpeedUpEffect(string source, SpeedUpEffectConfig config)
        : base(source, config.Duration, EffectType.Unique, EffectTypeId.SlowEffect)
    {
        _config = config;
    }

    public override void OverrideEffect(StatusEffect existEffect)
    {
        base.OverrideEffect(existEffect);

        if (existEffect is SpeedUpEffect speedUpEffect)
        {
            base.OverrideEffect(existEffect);
            existEffect.ResetDuration(Duration);
            speedUpEffect._config.SpeedUpPercentage = Math.Max(speedUpEffect._config.SpeedUpPercentage, _config.SpeedUpPercentage);
        }
    }

    public override int CompareTo(StatusEffect other)
    {
        if (other is SpeedUpEffect speedUpEffect)
        {
            return _config.SpeedUpPercentage.CompareTo(speedUpEffect._config.SpeedUpPercentage);
        }

        throw new ArgumentException("Cannot compare SpeedUpEffect with non-SpeedUpEffect");
    }

    public override void EffectEnd()
    {
        _config.Stats.MoveSpeed.RemoveModifiersFromSource(Source);

        _config.Movement.MoveAnimationSpeed = 1;
    }

    public override void EffectStart()
    {
        StatModifier modifier = StatModifier.Create(Source, _config.SpeedUpPercentage, StatModifierType.PercentMult);

        _config.Stats.MoveSpeed.AddModifier(modifier);

        _config.Movement.MoveAnimationSpeed = 1 + _config.SpeedUpPercentage;
    }
}

[Serializable]
public class SpeedUpEffectConfig
{
    public float Duration;
    [Range(0.1f, 1)] public float SpeedUpPercentage;

    public IStats Stats;
    public IMovement Movement;
}