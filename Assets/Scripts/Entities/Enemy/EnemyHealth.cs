public class EnemyHealth : EntityHealth
{
    private Enemy enemy;

    public override void Awake()
    {
        base.Awake();

        enemy = GetComponentInParent<Enemy>();
    }

    public override void TakeDamage(DameInstance info)
    {
        base.TakeDamage(info);
    }
}
