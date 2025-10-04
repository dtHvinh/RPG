
using UnityEngine;

public class Enemy_StunnedState : EnemyState
{
    public Enemy_StunnedState(EntityStateMachine stateMachine, Enemy enemy, string animationBoolName)
        : base(stateMachine, enemy, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        SetTimer(enemy.Combat.GetStunDuration());

        Vector2 stunVel = enemy.Combat.GetStunVelocity();
        enemy.Movement.SetVelocity(stunVel.x * -enemy.FacingDirection, stunVel.y);
    }

    public override void Update()
    {
        base.Update();

        if (IsTimerFinished())
        {
            if (enemy.Combat.GetTarget() != null)
            {
                stateMachine.ChangeState(enemy.BattleState);
            }
            else
            {
                stateMachine.ChangeState(enemy.IdleState);
            }
        }
    }
}
