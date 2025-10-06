using UnityEngine;

public class BurnEffect : StatusEffect
{
    private readonly BurnEffectConfig _config;

    private readonly DameResult dameResult;
    private readonly DameInstance dameInstance;

    public BurnEffect(string source, BurnEffectConfig burnConfig)
        : base(source, burnConfig.Duration, EffectType.Stackable, EffectTypeId.BurnEffect)
    {
        _config = burnConfig;

        dameResult = new DameResult(DameTypes.TrueDame, _config.DamePerSec, false, 1);
        dameInstance = new(dameResult, ArmorPenetrationResult.Zero, _config.DameDealer);
    }

    public override int CompareTo(StatusEffect other)
    {
        return 0;
    }

    public override void EffectEnd()
    {
    }

    public override void OnTick(float delta, bool second)
    {
        base.OnTick(delta, second);

        if (second)
        {
            Debug.Log($"Burn for {_config.DamePerSec} dame");
            _config.Health.TakeDamage(dameInstance);
        }
    }

    public override void EffectStart()
    {
    }
}

public class BurnEffectConfig
{
    public float Duration;
    public IHealth Health;
    public IEntity DameDealer;
    public float DamePerSec;
}