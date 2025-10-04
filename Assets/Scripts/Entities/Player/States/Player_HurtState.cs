public class Player_HurtState : PlayerState
{
    public const string STATE_NAME = "hurt";

    public Player_HurtState(EntityStateMachine stateMachine, Player player, string animationBoolName)
        : base(stateMachine, player, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.Movement.FacingToTarget(player.Combat.GetTarget());
        player.Movement.Stop();
        player.Combat.ApplyKnockback(player.Combat.CalculateKnockbackDirection(player.Combat.GetTarget()));
    }

    public override void Update()
    {
        base.Update();

        if (triggerCalled)
            stateMachine.ChangeState(player.IdleState);
    }
}
