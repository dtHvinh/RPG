using UnityEngine;

public class PlayerState : EntityState
{
    protected Player player;

    public Vector2 MoveInput => player.MoveInput;
    public PlayerInputSet Inputs => player.Inputs;

    public PlayerState(EntityStateMachine stateMachine, Player player, string animationBoolName)
        : base(stateMachine, player, animationBoolName)
    {
        this.player = player;
    }

    public override void Update()
    {
        base.Update();

        Animator.SetFloat(AnimatorConstants.Y_VELOCITY, Rb.linearVelocityY);

        if (player.Inputs.Player.Dash.WasPressedThisFrame() && CanDash())
        {
            stateMachine.ChangeState(player.DashState);
        }
    }

    private bool CanDash()
    {
        if (player.WallDetected)
            return false;

        if (stateMachine.CurrentState == player.DashState)
            return false;

        return true;
    }
}
