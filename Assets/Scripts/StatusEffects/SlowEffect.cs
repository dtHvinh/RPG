using System;

/**
 * Slow effect is unique, it will override the current slow effect if the new one is stronger. and finally,
 * it will reset the duration.
 */
public class SlowEffect : StatusEffect
{
    private readonly SlowEffectConfig _config;

    public SlowEffect(Type source, SlowEffectConfig config)
        : base(source, config.Duration, EffectType.Unique, EffectTypeId.SlowEffect)
    {
        _config = config;
    }

    public override void EffectStart()
    {
        StatModifier modifier = StatModifier.Create(Source, _config.SlowPercentage * -1, StatModifierType.PercentMult);

        _config.Stats.MoveSpeed.AddModifier(modifier);
    }

    public override void EffectEnd()
    {
        _config.Stats.MoveSpeed.RemoveModifiersFromSource(Source);
    }

    public override int CompareTo(StatusEffect other)
    {
        if (other is SlowEffect otherSlow)
        {
            return _config.SlowPercentage.CompareTo(otherSlow._config.SlowPercentage);
        }

        throw new ArgumentException("Cannot compare SlowEffect with non-SlowEffect");
    }

    public override void OverrideEffect(StatusEffect existEffect)
    {
        base.OverrideEffect(existEffect);

        if (existEffect is SlowEffect existSlowEffect)
        {
            existSlowEffect.EffectEnd();
            existSlowEffect.ResetDuration(_config.Duration);
            existSlowEffect._config.SlowPercentage = Math.Max(existSlowEffect._config.SlowPercentage, _config.SlowPercentage);
        }
    }
}

public class SlowEffectConfig
{
    public float SlowPercentage;
    public IStats Stats;
    public float Duration;

    public SlowEffectConfig(float slowPercentage, float duration, IStats stats)
    {
        SlowPercentage = Math.Abs(slowPercentage);
        Stats = stats;
        Duration = duration;
    }
}
