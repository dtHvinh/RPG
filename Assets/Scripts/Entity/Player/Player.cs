using UnityEngine;

public class Player : Entity
{
    public PlayerInputSet Inputs { get; private set; }
    public Vector2 MoveInput { get; private set; }

    private EntityStateMachine stateMachine;
    public Player_IdleState IdleState { get; private set; }
    public Player_MoveState MoveState { get; private set; }
    public Player_JumpState JumpState { get; private set; }
    public Player_GroundState GroundState { get; private set; }
    public Player_FallState FallState { get; private set; }
    public Player_WallSlideState WallSlideState { get; private set; }
    public Player_WallJumpState WallJumpState { get; private set; }
    public Player_DashState DashState { get; private set; }
    public Player_BasicAttackState BasicAttackState { get; private set; }

    [Header("Movement Details")]
    public float JumpAirResistance = 0.8f;
    public float DashSpeed = 20f;
    public float DashDuration = 0.2f;

    [Header("Collision Detection")]
    public bool WallDetected { get; private set; } = false;
    public float WallCheckDistance = 0.43f;

    [Header("Attack Details")]
    public Vector2 attackVelocity;
    public float attackVelocityDuration;
    public float comboResetTime = 1f;

    private void Awake()
    {
        Inputs = new PlayerInputSet();

        stateMachine = new EntityStateMachine();

        IdleState = new Player_IdleState(stateMachine, this, Player_IdleState.STATE_NAME);
        MoveState = new Player_MoveState(stateMachine, this, Player_MoveState.STATE_NAME);
        JumpState = new Player_JumpState(stateMachine, this, Player_JumpState.STATE_NAME);
        FallState = new Player_FallState(stateMachine, this, Player_FallState.STATE_NAME);
        WallSlideState = new Player_WallSlideState(stateMachine, this, Player_WallSlideState.STATE_NAME);
        WallJumpState = new Player_WallJumpState(stateMachine, this, Player_WallJumpState.STATE_NAME);
        DashState = new Player_DashState(stateMachine, this, Player_DashState.STATE_NAME);
        BasicAttackState = new Player_BasicAttackState(stateMachine, this, Player_BasicAttackState.STATE_NAME);
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

        Debug.Log(stateMachine.currentState.AnimationBoolName);

        stateMachine.ActiveStateUpdate();
    }

    protected override void HandleCollisionDetection()
    {
        base.HandleCollisionDetection();

        WallDetected = Physics2D.Raycast(transform.position, Vector2.right * FacingDirection, WallCheckDistance, groundLayer);
    }

    public override void Start()
    {
        base.Start();

        stateMachine.Initialize(IdleState);
    }

    public void CallAnimationTrigger() => stateMachine.currentState.CallAnimationTrigger();

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + FacingDirection * WallCheckDistance * Vector3.right);
    }
}
