using System.Collections;
using UnityEngine;

public class Player : Entity
{
    private Coroutine queuedAttackCo;

    public PlayerInputSet Inputs { get; private set; }
    public Vector2 MoveInput { get; private set; }

    public Player_IdleState IdleState { get; private set; }
    public Player_MoveState MoveState { get; private set; }
    public Player_JumpState JumpState { get; private set; }
    public Player_GroundState GroundState { get; private set; }
    public Player_FallState FallState { get; private set; }
    public Player_WallSlideState WallSlideState { get; private set; }
    public Player_WallJumpState WallJumpState { get; private set; }
    public Player_DashState DashState { get; private set; }
    public Player_BasicAttack1State BasicAttack1State { get; private set; }
    public Player_BasicAttack2State BasicAttack2State { get; private set; }
    public Player_BasicAttack3State BasicAttack3State { get; private set; }
    public Player_PlungeAttackState PlungeAttackState { get; private set; }

    [Header("Movement Details")]
    public float JumpAirResistance = 0.8f;
    public float DashSpeed = 20f;
    public float DashDuration = 0.2f;

    [Header("Attack Details")]
    public Vector2[] AttackVelocity;
    public Vector2 PlungeAttackVelocity;
    public float attackVelocityDuration;
    public float comboResetTime = 1f;

    public override void Awake()
    {
        base.Awake();

        Inputs = new PlayerInputSet();
    }

    public override void InitializeStates()
    {
        IdleState = new Player_IdleState(stateMachine, this, Player_IdleState.STATE_NAME);
        MoveState = new Player_MoveState(stateMachine, this, Player_MoveState.STATE_NAME);
        JumpState = new Player_JumpState(stateMachine, this, Player_JumpState.STATE_NAME);
        FallState = new Player_FallState(stateMachine, this, Player_FallState.STATE_NAME);
        WallSlideState = new Player_WallSlideState(stateMachine, this, Player_WallSlideState.STATE_NAME);
        WallJumpState = new Player_WallJumpState(stateMachine, this, Player_WallJumpState.STATE_NAME);
        DashState = new Player_DashState(stateMachine, this, Player_DashState.STATE_NAME);
        BasicAttack1State = new Player_BasicAttack1State(stateMachine, this, Player_BasicAttackState.STATE_NAME);
        BasicAttack2State = new Player_BasicAttack2State(stateMachine, this, Player_BasicAttackState.STATE_NAME);
        BasicAttack3State = new Player_BasicAttack3State(stateMachine, this, Player_BasicAttackState.STATE_NAME);
        PlungeAttackState = new Player_PlungeAttackState(stateMachine, this, Player_PlungeAttackState.STATE_NAME);
    }

    public override void SetInitialState()
    {
        stateMachine.Initialize(IdleState);
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

    protected override void Update()
    {
        base.Update();
    }

    private IEnumerator EnterAttackStateCo(Player_BasicAttackState nextAttackState)
    {
        yield return new WaitForEndOfFrame();
        stateMachine.ChangeState(nextAttackState);
    }

    public void ChangeAttackState(Player_BasicAttackState nextAttackState)
    {
        if (queuedAttackCo != null)
            StopCoroutine(queuedAttackCo);
        queuedAttackCo = StartCoroutine(EnterAttackStateCo(nextAttackState));
    }

    public void CallAnimationTrigger() => stateMachine.CurrentState.CallAnimationTrigger();
}
