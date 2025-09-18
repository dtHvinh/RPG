public class Player_GroundState : PlayerState
{
    public Player_GroundState(EntityStateMachine stateMachine, Player entity, string animationBoolName) : base(stateMachine, entity, animationBoolName)
    {
    }

    public override void Update()
    {
        base.Update();

        if (Inputs.Player.Jump.WasPerformedThisFrame())
        {
            stateMachine.ChangeState(player.JumpState);
        }

        if(Inputs.Player.Attack.WasPerformedThisFrame())
        {
            stateMachine.ChangeState(player.BasicAttackState);
        }
    }
}
