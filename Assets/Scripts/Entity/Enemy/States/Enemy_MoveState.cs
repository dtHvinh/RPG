using UnityEngine;

public class Enemy_MoveState : EnemyState
{
    public Enemy_MoveState(EntityStateMachine stateMachine, Enemy entity, string animationBoolName)
        : base(stateMachine, entity, animationBoolName)
    {
    }

    public override void Update()
    {
        base.Update();

        if (enemy.CanMove())
        {
            enemy.MoveWithBaseSpeed();
        }
        else
        {
            stateMachine.ChangeState(enemy.IdleState);
        }
    }
}
