using System.Linq;
using UnityEngine;

public abstract class EnemyBase : EntityBase
{
    [Header("Enemy Movement Settings")]
    [Range(0, 2)] public float MoveAnimSpeedMulti = 1f;
    [Range(0, 2)] public float BattleAnimSpeedMulti = 1f;
    public float IdleTime = 2f;

    [Header("Target Detection")]
    [SerializeField] protected float detectionDistance = 10f;
    [SerializeField] protected LayerMask detectionTargetLayers;
    [SerializeField] protected Transform detectionCheckPoint;

    [Header("Battle Settings")]
    public float battleMoveSpeedMulti = 1.5f;

    public Enemy_IdleState IdleState { get; protected set; }
    public Enemy_MoveState MoveState { get; protected set; }
    public Enemy_AttackState AttackState { get; protected set; }
    public Enemy_BattleState BattleState { get; protected set; }

    public EnemyAI AI { get; protected set; }

    public RaycastHit2D[] DetectTargets()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(
            origin: detectionCheckPoint.position,
            direction: Vector2.right * FacingDirection,
            distance: detectionDistance,
            layerMask: detectionTargetLayers | groundLayer);

        return hits
            .OrderBy(h => h.distance)
            .TakeWhile(h => h.collider != null && ((1 << h.collider.gameObject.layer) & groundLayer) == 0).ToArray();
    }

    public int DetectTargets(RaycastHit2D[] hits)
    {
        return Physics2D.RaycastNonAlloc(
            origin: detectionCheckPoint.position,
            direction: Vector2.right * FacingDirection,
            distance: detectionDistance,
            results: hits,
            layerMask: detectionTargetLayers | groundLayer);
    }

    public RaycastHit2D DetectTarget()
    {
        RaycastHit2D hit = Physics2D.Raycast(
            origin: detectionCheckPoint.position,
            direction: Vector2.right * FacingDirection,
            distance: detectionDistance,
            layerMask: detectionTargetLayers | groundLayer);

        if (hit.collider != null && ((1 << hit.collider.gameObject.layer) & groundLayer) == 0)
            return hit;

        return default;
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.red;
        Gizmos.DrawLine(detectionCheckPoint.position, detectionCheckPoint.position + Vector3.right * FacingDirection * detectionDistance);
    }
}
