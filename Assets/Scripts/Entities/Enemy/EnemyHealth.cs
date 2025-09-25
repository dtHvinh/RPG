public class EnemyHealth : EntityHealth
{
    private Enemy enemy;

    private void Awake()
    {
        enemy = GetComponentInParent<Enemy>();
    }

    public override void TakeDamage(DameDealingInfo info)
    {
        if (info.dameDealer.CompareTag(Tags.Player))
        {
            enemy.TryEnterBattleState(info.dameDealer.transform);
        }
        base.TakeDamage(info);
    }
}
