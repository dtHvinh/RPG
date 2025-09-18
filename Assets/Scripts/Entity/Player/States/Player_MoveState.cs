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

        if (player.CanMove)
        {
            player.SetVelocity(
                xVelocity: MoveInput.x * Stats.GetMoveSpeed(),
                yVelocity: Rb.linearVelocity.y);

            if (!player.IsHorizontallyMoving())
            {
                stateMachine.ChangeState(player.IdleState);
            }

            if (!player.GroundDetected)
            {
                stateMachine.ChangeState(player.FallState);
            }
        }
    }
}
