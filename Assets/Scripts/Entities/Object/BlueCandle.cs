using UnityEngine;

public class BlueCandle : AreaBuffingObjectFloating, IEffectSource
{
    [SerializeField] private SpeedUpEffectSO speedUpEffectConfig;
    private SpeedUpEffectConfig config;

    protected override void Awake()
    {
        base.Awake();

        config = new()
        {
            SpeedUpPercentage = speedUpEffectConfig.SpeedUpPercentage,
            Duration = speedUpEffectConfig.Duration
        };
    }
    public string GetEffectSourceName() => "Blue Candle";

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<HitBox>(out var hitBox)
            && IsSpeedUpApplicable(hitBox, out var stats, out var movement, out var statusHandler))
        {
            config.Stats = stats;
            config.Movement = movement;

            SpeedUpEffect speedUpEffect = new(GetEffectSourceName(), config);
            statusHandler.Effects.AddEffect(speedUpEffect);
        }
    }

    private bool IsSpeedUpApplicable(HitBox hitBox, out IStats stats, out IMovement movement, out EntityStatusHandler statusHandler)
    {
        stats = hitBox.GetStats();
        movement = hitBox.GetComponentInParent<IMovement>();
        statusHandler = hitBox.GetStatusHandler();

        return stats != null && movement != null && statusHandler != null;
    }
}
