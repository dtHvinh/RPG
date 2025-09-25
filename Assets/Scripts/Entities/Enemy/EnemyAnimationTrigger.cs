public class EnemyAnimationTrigger : EntityAnimationTriggers
{
    private Enemy enemy;

    protected override void Awake()
    {
        enemy = GetComponentInParent<Enemy>();
        entity = enemy;
    }

    protected void AttackTrigger()
    {
        enemy.Combat.PerformAttack();
    }
}
