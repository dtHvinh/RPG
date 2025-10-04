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

        player.SetVelocity(Rb.linearVelocityX * player.Movement.JumpAirResistance, player.Stats.JumpForce);
    }

    public override void Update()
    {
        base.Update();

        if (Rb.linearVelocityY <= 0
            // Avoid transitioning to fall state if we're in the middle of a plunge attack
            && stateMachine.CurrentState != player.PlungeAttackState)
        {
            stateMachine.ChangeState(player.FallState);
        }
    }
}
