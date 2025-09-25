using UnityEngine;

public class EntityCombat : MonoBehaviour
{
    private Entity entity;
    private Transform targetTransform;

    // TODO: Move to stats
    private float dame = 10;

    [Header("Target detection")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private LayerMask targetLayers;

    private void Awake()
    {
        entity = GetComponentInParent<Entity>();
    }

    private Collider2D[] GetDetectedColliders()
    {
        return Physics2D.OverlapCircleAll(attackPoint.position, entity.Stats.AttackRadius, targetLayers);
    }

    public void SetTarget(Transform target)
    {
        if (target != null && targetTransform == null)
        {
            targetTransform = target;
        }
    }

    public Transform GetTarget() => targetTransform;

    public void ClearTarget() => targetTransform = null;

    public float GetAttackDistance() => transform.DistanceXTo(attackPoint.transform);

    public bool WithinAttackDistance(Transform target) => transform.DistanceXTo(target) <= GetAttackDistance();

    public bool WithinAttackDistance() => targetTransform != null && WithinAttackDistance(targetTransform);

    public Transform GetAttackPoint()
    {
        return attackPoint;
    }

    public float DirectionToTarget(Transform target) => Mathf.Sign(target.position.x - entity.transform.position.x);

    public float DirectionToTarget() => targetTransform != null ? DirectionToTarget(targetTransform) : entity.FacingDirection;

    public void FacingToTarget(Transform target)
    {
        if (entity.FacingDirection != DirectionToTarget(target))
            entity.Movement.Flip();
    }

    public void FacingToTarget()
    {
        if (targetTransform != null)
            FacingToTarget(targetTransform);
    }

    public void PerformAttack()
    {
        foreach (var collider in GetDetectedColliders())
        {
            if (collider.TryGetComponent<Entity>(out var target))
                target.Health.TakeDamage(new DameDealingInfo()
                {
                    damage = dame,
                    dameDealer = entity.transform,
                });
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        try
        {
            _ = entity.Stats.AttackRadius;
            Gizmos.DrawWireSphere(attackPoint.WithLabel("Attack Range", Color.red).position, entity.Stats.AttackRadius);
        }
        catch
        {
        }
    }
}
