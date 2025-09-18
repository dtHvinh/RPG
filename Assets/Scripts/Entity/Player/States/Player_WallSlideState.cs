public class Player_WallSlideState : PlayerState
{
    public const string STATE_NAME = "wallSlide";

    public Player_WallSlideState(EntityStateMachine stateMachine, Player player, string animationBoolName)
        : base(stateMachine, player, animationBoolName)
    {
    }

    public override void Update()
    {
        base.Update();

        HandleWallSlideMovement();

        if (player.WallDetected == false)
        {
            stateMachine.ChangeState(player.FallState);
            return;
        }

        if (player.GroundDetected)
        {
            stateMachine.ChangeState(player.IdleState);
            player.Flip();
        }

        if (Inputs.Player.Jump.WasPressedThisFrame())
        {
            stateMachine.ChangeState(player.WallJumpState);
            return;
        }
    }

    private void HandleWallSlideMovement()
    {
        if (player.MoveInput.y < 0)
        {
            player.SetVelocity(player.MoveInput.x, Rb.linearVelocityY);
        }
        else
        {
            player.SetVelocity(player.MoveInput.x, Rb.linearVelocityY * .3f);
        }
    }
}
