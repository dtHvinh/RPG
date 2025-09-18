using System;
using System.Collections;
using UnityEngine;

public class Player_BasicAttackState : PlayerState
{
    public const string STATE_NAME = "basicAttack";
    public const int FirstComboIndex = 1;
    public const int SecondComboIndex = 2;
    public const int LastComboIndex = 3;

    private float attackVelocityTimer;
    private int comboIndex = 1;

    private float lastTimeAttacked;

    public Player_BasicAttackState(EntityStateMachine stateMachine, Player player, string animationBoolName)
        : base(stateMachine, player, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        ResetComboIfNeed();

        player.Animator.SetInteger(AnimatorConstants.BASIC_ATTACK_INDEX, comboIndex);

        ApplyAttackVelocity();
    }

    private void ResetComboIfNeed()
    {
        if (Time.time - lastTimeAttacked > player.comboResetTime)
        {
            comboIndex = FirstComboIndex;
        }
        else if (comboIndex > LastComboIndex)
        {
            comboIndex = FirstComboIndex;
        }
    }

    public override void Update()
    {
        base.Update();
        HandleAttackBodyVelocity();

        if (triggerCalled)
        {
            stateMachine.ChangeState(player.IdleState);
        }
    }

    public override void Exit()
    {
        base.Exit();

        lastTimeAttacked = Time.time;
        comboIndex++;
    }

    private void HandleAttackBodyVelocity()
    {
        attackVelocityTimer -= Time.deltaTime;

        if (attackVelocityTimer < 0)
        {
            player.SetVelocity(0, Rb.linearVelocityY);
        }
    }

    private void ApplyAttackVelocity()
    {
        attackVelocityTimer = player.attackVelocityDuration;

        player.SetVelocity(
            xVelocity: player.attackVelocity.x * player.FacingDirection,
            yVelocity: player.attackVelocity.y);
    }
}
