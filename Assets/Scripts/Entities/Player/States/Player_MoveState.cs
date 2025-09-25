public class Player_MoveState : Player_GroundState
{
    public const string STATE_NAME = "move";

    public Player_MoveState(EntityStateMachine stateMachine, Player entity, string animationBoolName)
        : base(stateMachine, entity, animationBoolName)
    {
    }

    public override void Update()
    {
        base.Update();

        if (player.MoveInput.x == 0 || player.Collision.WallDetected)
        {
            stateMachine.ChangeState(player.IdleState);
        }

        player.SetVelocity(
            xVelocity: MoveInput.x * player.Stats.MoveSpeed,
            yVelocity: Rb.linearVelocity.y);

        if (!player.Collision.GroundDetected)
        {
            stateMachine.ChangeState(player.FallState);
        }
    }
}
