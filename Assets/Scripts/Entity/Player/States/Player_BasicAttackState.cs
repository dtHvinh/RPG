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
    private float attackDir;

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

    private void QueueNextAttack()
    {
        if (comboIndex < LastComboIndex)
        {
            attackComboQueued = true;
        }
    }

    public override void Update()
    {
        base.Update();

        HandleAttackBodyVelocity();

        attackDir = player.MoveInput.x != 0 ? player.MoveInput.x : player.FacingDirection;

        if (Inputs.Player.Attack.WasPressedThisFrame())
        {
            QueueNextAttack();
        }

        if (triggerCalled)
        {
            if (attackComboQueued)
            {
                Animator.SetBool(AnimationBoolName, false);
                player.EnterAttackStateWithDelay();
            }
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
                xVelocity: player.AttackVelocity[comboIndex - 1].x * attackDir,
                yVelocity: player.AttackVelocity[comboIndex - 1].y);
    }
}
