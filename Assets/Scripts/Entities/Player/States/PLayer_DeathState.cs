public class Player_DeathState : PlayerState
{
    public const string STATE_NAME = "death";

    public Player_DeathState(EntityStateMachine stateMachine, Player player, string animationBoolName)
        : base(stateMachine, player, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.Movement.Stop();

        player.Inputs.Disable();

        UnityEngine.Object.Destroy(player.gameObject, 2f);
    }
}
