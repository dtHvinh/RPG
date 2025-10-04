using UnityEngine;

[RequireComponent(typeof(IStats))]
public class EntityStatusHandler : MonoBehaviour
{
    private IStats stats;
    [SerializeField] private StatusEffectList effects;
    private float accumulator = 0;

    public float E { get; } = 10;

    private void Awake()
    {
        effects = new();

        stats = GetComponent<IStats>();
    }

    private void Update()
    {
        accumulator += Time.deltaTime;
        if (accumulator >= 1f)
        {
            effects.Tick();
            accumulator = 0;
        }
    }

    [ContextMenu("Apply Slow Effect")]
    public void Temp1()
    {
        SlowEffectConfig config = new(.5f, 5, stats);
        SlowEffect slowEffect = new(typeof(EntityStatusHandler), config);

        effects.AddEffect(slowEffect);
    }
}
