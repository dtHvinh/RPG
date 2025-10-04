using UnityEngine;

[RequireComponent(typeof(EntityStats))]
[RequireComponent(typeof(EnemyAI))]
[RequireComponent(typeof(EnemyCombat))]
[RequireComponent(typeof(EnemyMovement))]
[RequireComponent(typeof(EntityCollision))]
[RequireComponent(typeof(EnemyHealth))]
public abstract class Enemy : Entity,
    IHasCombat<EnemyCombat>,
    IHasMovement<EnemyMovement>,
    IHasCollision<EntityCollision>,
    IHasHealth<EnemyHealth>,
    IHasStats<EntityStats>,
    IHasAI<EnemyAI>
{
    [Header("Battle Settings")]
    public float battleMoveSpeedMulti = 1.5f;

    public Enemy_IdleState IdleState { get; protected set; }
    public Enemy_MoveState MoveState { get; protected set; }
    public Enemy_AttackState AttackState { get; protected set; }
    public Enemy_BattleState BattleState { get; protected set; }
    public Enemy_HurtState HurtState { get; protected set; }
    public Enemy_DeathState DeathState { get; protected set; }
    public Enemy_StunnedState StunnedState { get; protected set; }

    public EnemyAI AI { get; protected set; }
    public EnemyCombat Combat { get; protected set; }
    public EnemyMovement Movement { get; protected set; }
    public EntityCollision Collision { get; protected set; }
    public EnemyHealth Health { get; protected set; }
    public EntityStats Stats { get; protected set; }

    public override void Awake()
    {
        base.Awake();

        AI = GetComponent<EnemyAI>();
        Combat = GetComponent<EnemyCombat>();
        Movement = GetComponent<EnemyMovement>();
        Collision = GetComponent<EntityCollision>();
        Health = GetComponent<EnemyHealth>();
        Stats = GetComponent<EntityStats>();
    }

    public override float FacingDirection => Movement.FacingDirection;

    public override void InitializeStates()
    {

    }

    public override void SubscribeToEvents()
    {
        base.SubscribeToEvents();

        Health.OnHurt += EnemyHealth_OnHurt;
        Health.OnDeath += EnemyHealth_OnDeath;
    }

    private void EnemyHealth_OnDeath(object sender, DeathEventArgrs e)
    {
        Death();
    }

    private void EnemyHealth_OnHurt(object sender, EntityHurtEventArgs e)
    {
        Combat.SetTarget(e.Hurt.DameDealer.Transform);
        TryEnterState(HurtState);
    }

    public override void Death()
    {
        TryEnterState(DeathState);
    }

    public override void SetInitialState()
    {
        StateMachine.Initialize(MoveState);
    }

    public override void SetVelocity(float xVelocity, float yVelocity) => Movement.SetVelocity(xVelocity, yVelocity);
}
