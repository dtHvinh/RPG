using UnityEngine;

[RequireComponent(typeof(EntityStats))]
public abstract class Enemy : Entity, IEntity<EnemyCombat, EnemyMovement, EntityCollision, EnemyHealth>
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
    public EnemyCombat Combat { get; protected set; }
    public EnemyMovement Movement { get; protected set; }
    public EntityCollision Collision { get; protected set; }
    public EnemyHealth Health { get; protected set; }
    public EntityStats Stats { get; protected set; }

    public override void Awake()
    {
        base.Awake();

        AI = new EnemyAI(this);

        Combat = GetComponentInChildren<EnemyCombat>();
        Movement = GetComponentInChildren<EnemyMovement>();
        Collision = GetComponentInChildren<EntityCollision>();
        Health = GetComponentInChildren<EnemyHealth>();
        Stats = GetComponent<EntityStats>();
    }

    public override float FacingDirection => Movement.FacingDirection;

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

    public override void SetVelocity(float xVelocity, float yVelocity) => Movement.SetVelocity(xVelocity, yVelocity);
}
