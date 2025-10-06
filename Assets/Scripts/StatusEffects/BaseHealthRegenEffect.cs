public class BaseHealthRegenEffect : StatusEffect
{
    private readonly IntervalTimer _interVal;
    private readonly BaseHealthRegenEffectConfig _config;

    public BaseHealthRegenEffect(string source, BaseHealthRegenEffectConfig conf)
        : base(source, InfiniteDuration, EffectType.Unique, EffectTypeId.BaseHealthRegenEffect)
    {
        _config = conf;
        _interVal = new IntervalTimer();
    }

    public override int CompareTo(StatusEffect other)
    {
        return 0;
    }

    public override void OnTick(float delta, bool second)
    {
        base.OnTick(delta, second);

        _interVal.Update(delta);

        if (_interVal.IsElapsed(_config.Stats.HealthInterval))
        {
            _config.Health.Regenerate(_config.Stats.HealthRegen * _config.Stats.HealthInterval);
        }
    }

    public override void EffectEnd()
    {
    }

    public override void EffectStart()
    {
    }
}

public class BaseHealthRegenEffectConfig
{
    public IStats Stats { get; private set; }
    public IHealth Health { get; private set; }

    public BaseHealthRegenEffectConfig(IStats stats, IHealth health)
    {
        Stats = stats;
        Health = health;
    }
}
