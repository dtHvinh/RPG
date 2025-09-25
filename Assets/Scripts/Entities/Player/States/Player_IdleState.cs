using UnityEngine;

public class Player_IdleState : Player_GroundState
{
    public const string STATE_NAME = "idle";

    public Player_IdleState(EntityStateMachine stateMachine, Player entity, string animationBoolName)
        : base(stateMachine, entity, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        Debug.Assert(player != null, "Player is null");

        player.SetVelocity(0, Rb.linearVelocityY);
    }

    public override void Update()
    {
        base.Update();

        if (player.MoveInput.x == player.FacingDirection && player.Collision.WallDetected)
        {
            return;
        }

        if (MoveInput.x != 0)
        {
            stateMachine.ChangeState(player.MoveState);
        }
    }
}
