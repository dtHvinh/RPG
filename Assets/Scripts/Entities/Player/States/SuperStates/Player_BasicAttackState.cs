using UnityEngine;

public class Player_BasicAttackState : PlayerState
{
    public const string STATE_NAME = "basicAttack";
    public const string BASIC_ATTACK_INDEX = "basicAttackIndex";

    private float attackVelocityTimer;
    private float attackDir;

    private Player_BasicAttackState nextAttackState;

    public Player_BasicAttackState(EntityStateMachine stateMachine, Player player, string animationBoolName)
        : base(stateMachine, player, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        attackDir = player.FacingDirection;
    }

    public override void Update()
    {
        base.Update();
        HandleAttackBodyVelocity();
        HandleUpdateAttackDirection();

        if (player.FacingDirection != attackDir)
            player.Movement.Flip();

        if (triggerCalled)
        {
            if (nextAttackState != null)
            {
                Animator.SetBool(AnimationBoolName, false);
                player.ChangeAttackState(nextAttackState);
            }
            else
                stateMachine.ChangeState(player.IdleState);
        }
    }

    public override void Exit()
    {
        base.Exit();

        nextAttackState = null;
    }

    public void ApplyAttackVelocity(Vector2 attackVelocity)
    {
        attackVelocityTimer = player.attackVelocityDuration;

        if (!player.Collision.CliffDetected)
            player.SetVelocity(
                xVelocity: attackVelocity.x * attackDir,
                yVelocity: attackVelocity.y);
    }

    public void QueueNextAttack(Player_BasicAttackState basicAttackState)
    {
        nextAttackState = basicAttackState;
    }

    private void HandleAttackBodyVelocity()
    {
        attackVelocityTimer -= Time.deltaTime;

        if (attackVelocityTimer < 0)
        {
            player.SetVelocity(0, Rb.linearVelocityY);
        }
    }

    private void HandleUpdateAttackDirection()
    {
        attackDir = player.MoveInput.x != 0 ? player.MoveInput.x : player.FacingDirection;
    }

    public static class BasicAttackConstants
    {
        public const int PLAYER_BASIC_ATTACK_1_INDEX = 1;
        public const int PLAYER_BASIC_ATTACK_2_INDEX = 2;
        public const int PLAYER_BASIC_ATTACK_3_INDEX = 3;
    }
}
