public class Player_JumpState : Player_AiredState
{
    public const string STATE_NAME = "jumpFall";

    public Player_JumpState(EntityStateMachine stateMachine, Player entity, string animationBoolName)
        : base(stateMachine, entity, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.SetVelocity(Rb.linearVelocityX * player.JumpAirResistance, Stats.GetJumpForce());
    }

    public override void Update()
    {
        base.Update();

        if (Rb.linearVelocityY <= 0)
        {
            stateMachine.ChangeState(player.FallState);
        }
    }
}
