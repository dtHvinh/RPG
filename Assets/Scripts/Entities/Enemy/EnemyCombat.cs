using UnityEngine;

public class EnemyCombat : EntityCombat
{
    private Enemy enemy;

    protected override void Awake()
    {
        enemy = GetComponentInParent<Enemy>();
        entity = enemy;
    }

    protected override Collider2D[] GetDetectedColliders()
    {
        return Physics2D.OverlapCircleAll(attackPoint.position, enemy.Stats.AttackRadius, targetLayers);
    }

    public override void PerformAttack()
    {
        foreach (var collider in GetDetectedColliders())
        {
            if (collider.TryGetComponent<Player>(out var target))
                target.Health.TakeDamage(new DameDealingInfo()
                {
                    damage = dame,
                    dameDealer = entity.transform,
                });
        }
    }

    public void FacingToTarget()
    {
        if (targetTransform != null)
            enemy.Movement.FacingToTarget(targetTransform);
    }

    public float DirectionToTarget() => targetTransform != null ? enemy.Movement.DirectionToTarget(targetTransform) : entity.FacingDirection;
}
