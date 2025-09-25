public class Player_DashState : PlayerState
{
    public const string STATE_NAME = "dash";
    private float originalGravity;

    public Player_DashState(EntityStateMachine stateMachine, Player player, string animationBoolName)
        : base(stateMachine, player, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = player.DashDuration;

        originalGravity = Rb.gravityScale;
        Rb.gravityScale = 0;
    }

    public override void Update()
    {
        base.Update();

        player.SetVelocity(player.FacingDirection * player.DashSpeed, 0);

        if (stateTimer < 0)
        {
            if (player.Collision.GroundDetected)
                stateMachine.ChangeState(player.IdleState);
            else
                stateMachine.ChangeState(player.FallState);
        }
    }

    public override void Exit()
    {
        base.Exit();

        player.SetVelocity(0, 0);

        Rb.gravityScale = originalGravity;
    }
}