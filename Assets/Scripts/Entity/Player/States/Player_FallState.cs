public class Player_FallState : Player_AiredState
{
    public const string STATE_NAME = "jumpFall";

    public Player_FallState(EntityStateMachine stateMachine, Player entity, string animationBoolName) : base(stateMachine, entity, animationBoolName)
    {
    }

    public override void Update()
    {
        base.Update();

        if (player.GroundDetected)
        {
            stateMachine.ChangeState(player.IdleState);
        }

        if (player.WallDetected)
        {
            stateMachine.ChangeState(player.WallSlideState);
        }
    }
}
