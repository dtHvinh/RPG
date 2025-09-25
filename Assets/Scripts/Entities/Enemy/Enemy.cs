using UnityEngine;

public abstract class Enemy : Entity
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

    public void TryEnterBattleState(Transform target)
    {
        if (StateMachine.CurrentState == BattleState
            || StateMachine.CurrentState == AttackState)
            return;

        Combat.SetTarget(target);
        StateMachine.ChangeState(BattleState);
    }

    public RaycastHit2D DetectTarget()
    {
        RaycastHit2D hit = Physics2D.Raycast(
            origin: detectionCheckPoint.position,
            direction: Vector2.right * FacingDirection,
            distance: detectionDistance,
            layerMask: detectionTargetLayers | Collision.GroundLayer);

        if (hit.collider != null && ((1 << hit.collider.gameObject.layer) & Collision.GroundLayer) == 0)
            return hit;

        return default;
    }
}
