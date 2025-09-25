using UnityEngine;

public class PlayerCombat : EntityCombat
{
    private Player player;

    protected override void Awake()
    {
        player = GetComponentInParent<Player>();
        entity = player;
    }

    protected override Collider2D[] GetDetectedColliders()
    {
        return Physics2D.OverlapCircleAll(attackPoint.position, player.Stats.AttackRadius, targetLayers);
    }

    public override void PerformAttack()
    {
        foreach (var collider in GetDetectedColliders())
        {
            if (collider.TryGetComponent<Enemy>(out var target))
                target.Health.TakeDamage(new DameDealingInfo()
                {
                    damage = dame,
                    dameDealer = entity.transform,
                });
        }
    }
}
