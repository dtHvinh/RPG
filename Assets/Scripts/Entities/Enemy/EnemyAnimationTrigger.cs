[UnityEngine.RequireComponent(typeof(EnemyVFX))]
public class EnemyAnimationTrigger : EntityAnimationTriggers
{
    private Enemy enemy;
    private EnemyVFX vfx;

    protected override void Awake()
    {
        enemy = GetComponentInParent<Enemy>();
        vfx = GetComponent<EnemyVFX>();
        entity = enemy;
    }

    protected void AttackTrigger()
    {
        enemy.Combat.PerformAttack();
    }

    private void EnableCounterWindow()
    {
        vfx.EnableAttackAlert(true);
        enemy.Combat.EnableCounterWindow(true);
    }

    private void DisableCounterWindow()
    {
        vfx.EnableAttackAlert(false);
        enemy.Combat.EnableCounterWindow(false);
    }
}
