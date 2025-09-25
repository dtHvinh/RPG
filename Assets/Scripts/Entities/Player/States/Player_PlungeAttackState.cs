using UnityEngine;

public class Player_PlungeAttackState : PlayerState
{
    public const string STATE_NAME = "plungeAttack";

    private bool isPlungeTouchGround; // One-time trigger when plunge attack touches the ground

    public Player_PlungeAttackState(EntityStateMachine stateMachine, Player player, string animationBoolName)
        : base(stateMachine, player, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.SetVelocity(player.PlungeAttackVelocity.x * player.FacingDirection, player.PlungeAttackVelocity.y);

        isPlungeTouchGround = false;
    }

    public override void Update()
    {
        base.Update();

        if (triggerCalled && player.Collision.GroundDetected)
        {
            stateMachine.ChangeState(player.IdleState);
        }

        if (player.Collision.GroundDetected && !isPlungeTouchGround)
        {
            player.OnlyMoveY();
            isPlungeTouchGround = true;
            Animator.SetTrigger(AnimatorConstants.PLUNGE_ATTACK_TRIGGER);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
