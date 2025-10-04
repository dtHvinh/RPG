using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EntityStats))]
[RequireComponent(typeof(PlayerCombat))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerCollision))]
[RequireComponent(typeof(EntityHealth))]
public class Player : Entity,
    IHasCombat<PlayerCombat>,
    IHasMovement<PlayerMovement>,
    IHasCollision<PlayerCollision>,
    IHasHealth<EntityHealth>,
    IHasStats<EntityStats>
{
    private Coroutine queuedAttackCo;

    public PlayerInputSet Inputs { get; private set; }
    public Vector2 MoveInput { get; private set; }

    public Player_IdleState IdleState { get; private set; }
    public Player_MoveState MoveState { get; private set; }
    public Player_JumpState JumpState { get; private set; }
    public Player_FallState FallState { get; private set; }
    public Player_WallSlideState WallSlideState { get; private set; }
    public Player_WallJumpState WallJumpState { get; private set; }
    public Player_DashState DashState { get; private set; }
    public Player_BasicAttack1State BasicAttack1State { get; private set; }
    public Player_BasicAttack2State BasicAttack2State { get; private set; }
    public Player_BasicAttack3State BasicAttack3State { get; private set; }
    public Player_PlungeAttackState PlungeAttackState { get; private set; }
    public Player_HurtState HurtState { get; private set; }
    public Player_DeathState DeathState { get; private set; }
    public Player_SlideState SlideState { get; private set; }
    public Player_CounterAttackState CounterState { get; private set; }

    public PlayerCombat Combat { get; private set; }
    public PlayerMovement Movement { get; private set; }
    public PlayerCollision Collision { get; private set; }
    public EntityHealth Health { get; private set; }
    public EntityStats Stats { get; private set; }

    public override float FacingDirection => Movement.FacingDirection;

    [Header("Attack Details")]
    public Vector2[] AttackVelocity;
    public Vector2 PlungeAttackVelocity;
    public float attackVelocityDuration;
    public float comboResetTime = 1f;

    public override void Awake()
    {
        base.Awake();

        Inputs = new PlayerInputSet();

        Combat = GetComponent<PlayerCombat>();
        Movement = GetComponent<PlayerMovement>();
        Collision = GetComponent<PlayerCollision>();
        Health = GetComponent<EntityHealth>();
        Stats = GetComponent<EntityStats>();
    }

    private void OnEnable()
    {
        Inputs.Enable();

        Inputs.Player.Movement.performed += ctx => MoveInput = ctx.ReadValue<Vector2>();
        Inputs.Player.Movement.canceled += ctx => MoveInput = Vector2.zero;
    }

    private void OnDisable()
    {
        Inputs.Disable();
    }

    public override void Update()
    {
        base.Update();
    }

    public override void InitializeStates()
    {
        IdleState = new Player_IdleState(StateMachine, this, Player_IdleState.STATE_NAME);
        MoveState = new Player_MoveState(StateMachine, this, Player_MoveState.STATE_NAME);
        JumpState = new Player_JumpState(StateMachine, this, Player_JumpState.STATE_NAME);
        FallState = new Player_FallState(StateMachine, this, Player_FallState.STATE_NAME);
        WallSlideState = new Player_WallSlideState(StateMachine, this, Player_WallSlideState.STATE_NAME);
        WallJumpState = new Player_WallJumpState(StateMachine, this, Player_WallJumpState.STATE_NAME);
        DashState = new Player_DashState(StateMachine, this, Player_DashState.STATE_NAME);
        BasicAttack1State = new Player_BasicAttack1State(StateMachine, this, Player_BasicAttackState.STATE_NAME);
        BasicAttack2State = new Player_BasicAttack2State(StateMachine, this, Player_BasicAttackState.STATE_NAME);
        BasicAttack3State = new Player_BasicAttack3State(StateMachine, this, Player_BasicAttackState.STATE_NAME);
        PlungeAttackState = new Player_PlungeAttackState(StateMachine, this, Player_PlungeAttackState.STATE_NAME);
        HurtState = new Player_HurtState(StateMachine, this, Player_HurtState.STATE_NAME);
        SlideState = new Player_SlideState(StateMachine, this, Player_SlideState.STATE_NAME);
        DeathState = new Player_DeathState(StateMachine, this, Player_DeathState.STATE_NAME);
        CounterState = new Player_CounterAttackState(StateMachine, this, Player_CounterAttackState.STATE_NAME);
    }

    public override void SetInitialState()
    {
        StateMachine.Initialize(IdleState);
    }

    public override void SubscribeToEvents()
    {
        base.SubscribeToEvents();

        Health.OnHurt += PlayerHealth_OnHurt;
        Health.OnDeath += PlayerHealth_OnDeath;
    }

    public override void Death()
    {
        TryEnterState(DeathState);
    }

    private void PlayerHealth_OnDeath(object sender, DeathEventArgrs e)
    {
        TryEnterState(DeathState);
    }

    private void PlayerHealth_OnHurt(object sender, EntityHurtEventArgs e)
    {
        Combat.SwitchTarget(e.Hurt.DameDealer.Transform);
        TryEnterState(HurtState);
    }

    public IEnumerator EnterAttackStateCo(Player_BasicAttackState nextAttackState)
    {
        yield return new WaitForEndOfFrame();
        StateMachine.ChangeState(nextAttackState);
    }

    public void ChangeAttackState(Player_BasicAttackState nextAttackState)
    {
        if (queuedAttackCo != null)
            StopCoroutine(queuedAttackCo);
        queuedAttackCo = StartCoroutine(EnterAttackStateCo(nextAttackState));
    }


    public override void SetVelocity(float xVelocity, float yVelocity) => Movement.SetVelocity(xVelocity, yVelocity);
}
