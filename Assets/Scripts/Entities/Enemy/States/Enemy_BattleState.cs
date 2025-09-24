using UnityEngine;

public class Enemy_BattleState : EnemyState
{
    public Transform target;

    public Enemy_BattleState(EntityStateMachine stateMachine, EnemyBase entity, string animationBoolName)
        : base(stateMachine, entity, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        enemy.Stats.MoveSpeed.AddPercentModifier(.5f);
    }

    public override void Update()
    {
        base.Update();

        target = enemy.DetectTarget().transform;

        if (target != null)
        {
            if (enemy.WithinAttackDistance(target))
            {
                stateMachine.ChangeState(enemy.AttackState);
            }
            else
            {
                if (enemy.AI.ShouldKeepChasingTarget())
                    enemy.SetVelocity(
                        enemy.Stats.MoveSpeed * DirectionToTarget(),
                        enemy.Rb.linearVelocityY);
                else
                    stateMachine.ChangeState(enemy.IdleState);
            }
        }
        else
        {
            stateMachine.ChangeState(enemy.IdleState);
        }
    }

    public override void Exit()
    {
        base.Exit();

        enemy.Stats.MoveSpeed.ClearModifiers();
    }

    private float DirectionToTarget() => Mathf.Sign(target.position.x - enemy.transform.position.x);
}
