using UnityEngine;

public abstract class EntityCombat : MonoBehaviour
{
    protected Entity entity;
    protected Transform targetTransform;

    // TODO: Move to stats
    protected float dame = 10;

    [Header("Target detection")]
    [SerializeField] protected Transform attackPoint;
    [SerializeField] protected LayerMask targetLayers;

    protected virtual void Awake()
    {
        entity = GetComponentInParent<Entity>();
    }

    protected abstract Collider2D[] GetDetectedColliders();

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

    public abstract void PerformAttack();
}
