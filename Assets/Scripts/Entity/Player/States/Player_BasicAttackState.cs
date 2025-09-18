using System;
using System.Collections;
using UnityEngine;

public class Player_BasicAttackState : PlayerState
{
    public const string STATE_NAME = "basicAttack";
    public const int FirstComboIndex = 1;
    public const int LastComboIndex = 3;

    private float attackVelocityTimer;
    private int comboIndex = 1;
    private bool attackComboQueued = false;

    private float lastTimeAttacked;

    public Player_BasicAttackState(EntityStateMachine stateMachine, Player player, string animationBoolName)
        : base(stateMachine, player, animationBoolName)
    {
        Debug.Assert(player.AttackVelocity.Length == LastComboIndex, "Attack velocity array length must be equal to LastComboIndex");
    }

    public override void Enter()
    {
        base.Enter();
        ResetComboIfNeed();
        attackComboQueued = false;
        player.Animator.SetInteger(AnimatorConstants.BASIC_ATTACK_INDEX, comboIndex);
        ApplyAttackVelocity();

    }

    private void ResetComboIfNeed()
    {
        if (Time.time - lastTimeAttacked > player.comboResetTime)
        {
            comboIndex = FirstComboIndex;
        }
    }

    private void IncreaseComboIndex()
    {
        comboIndex++;
        if (comboIndex > LastComboIndex)
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
            if(attackComboQueued)
                stateMachine.ChangeState(player.BasicAttackState);
            else
                stateMachine.ChangeState(player.IdleState); 
        }

        if(player.Inputs.Player.Attack.WasPressedThisFrame())
        {
            attackComboQueued = true;
        }
    }

    public override void Exit()
    {
        base.Exit();

        lastTimeAttacked = Time.time;
        IncreaseComboIndex();
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

        if (!player.CliffDetected)
            player.SetVelocity(
                xVelocity: player.AttackVelocity[comboIndex - 1].x * player.FacingDirection,
                yVelocity: player.AttackVelocity[comboIndex - 1].y);
    }
}
