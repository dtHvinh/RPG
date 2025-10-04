public class Player_SlideState : PlayerState
{
    public const string STATE_NAME = "slide";

    public Player_SlideState(EntityStateMachine stateMachine, Player player, string animationBoolName)
        : base(stateMachine, player, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        SetTimer(player.Movement.SlideDuration);
    }

    public override void Update()
    {
        base.Update();

        player.Movement.SetVelocity(player.Movement.SlideSpeed * player.FacingDirection, player.Rb.linearVelocityY);

        if (IsTimerFinished())
        {
            if (player.Collision.GroundDetected)
                stateMachine.ChangeState(player.IdleState);
            else
                stateMachine.ChangeState(player.FallState);
        }
    }
}
