public class Player_WallJumpState : Player_AiredState
{
    public const string STATE_NAME = "jumpFall";

    public Player_WallJumpState(EntityStateMachine stateMachine, Player player, string animationBoolName)
        : base(stateMachine, player, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        var jumpVector = player.Stats.GetWallJumpForce();

        player.SetVelocity(jumpVector.x * -player.FacingDirection * player.JumpAirResistance, jumpVector.y);
    }

    public override void Update()
    {
        base.Update();

        if (Rb.linearVelocityY < 0)
        {
            stateMachine.ChangeState(player.FallState);
        }
    }
}
