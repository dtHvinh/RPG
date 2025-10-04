using UnityEngine;

[RequireComponent(typeof(IStats))]
[RequireComponent(typeof(IEntity))]
[RequireComponent(typeof(ICollision))]
//TODO: Add chase distance limit
public class EnemyCombat : EntityCombat
{
    private ICollision collision;

    [Header("Target Detection")]
    [SerializeField] protected float detectionDistance = 10f;
    [SerializeField] protected LayerMask detectionTargetLayers;
    [SerializeField] protected Transform detectionCheckPoint;

    [Header("Stunned Details")]
    [SerializeField] private float stunnedDuration = 1f;
    [SerializeField] private Vector2 stunVelocity = new(2, 2);
    [SerializeField] protected bool canBeCounter = false;

    protected override void Awake()
    {
        base.Awake();

        collision = GetComponent<ICollision>();
    }

    public float GetStunDuration() => stunnedDuration;

    public Vector2 GetStunVelocity() => stunVelocity;

    public bool CanBeCounter() => canBeCounter;

    public void EnableCounterWindow(bool enable) => canBeCounter = enable;

    public void FacingToTarget()
    {
        if (targetTransform != null)
            movement.FacingToTarget(targetTransform);
    }

    public RaycastHit2D DetectTarget()
    {
        RaycastHit2D hit = Physics2D.Raycast(
            origin: detectionCheckPoint.position,
            direction: Vector2.right * movement.FacingDirection,
            distance: detectionDistance,
            layerMask: detectionTargetLayers | collision.GetGroundLayer());

        Debug.DrawRay(detectionCheckPoint.position, detectionDistance * movement.FacingDirection * Vector2.right, Color.red);

        if (hit.collider != null && ((1 << hit.collider.gameObject.layer) & collision.GetGroundLayer()) == 0)
            return hit;

        return default;
    }

    public float DirectionToTarget() => targetTransform != null ? movement.DirectionToTarget(targetTransform) : movement.FacingDirection;
}
