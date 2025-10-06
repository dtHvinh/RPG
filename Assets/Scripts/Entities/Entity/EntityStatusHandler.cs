using UnityEngine;
[RequireComponent(typeof(IStats))]
[RequireComponent(typeof(IHealth))]
public class EntityStatusHandler : MonoBehaviour, IStatusHandler
{
    private const string NATURAL_BUFFING_SOURCE = "Natural";
    [SerializeField] private StatusEffectList effects;

    public StatusEffectList Effects => effects;

    public float E { get; } = 10;

    private void Awake()
    {
        effects = new();
    }

    private void Start()
    {
        ApplyBaseRegeneration();
    }

    private void Update()
    {
        Effects.OnTick(Time.deltaTime);
    }

    private void ApplyBaseRegeneration()
    {
        IStats stats = GetComponent<IStats>();
        IHealth health = GetComponent<IHealth>();
        if (stats != null && health != null)
        {
            BaseHealthRegenEffectConfig regenConfig = new(stats, health);
            BaseHealthRegenEffect regenEffect = new(NATURAL_BUFFING_SOURCE, regenConfig);
            Effects.AddEffect(regenEffect);
        }
    }
}
