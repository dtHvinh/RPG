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

        if (CanMove())
        {
            enemy.MoveWithBaseSpeed();
        }
        else
        {
            enemy.Flip();
            stateMachine.ChangeState(enemy.IdleState);
        }
    }

    private bool CanMove() => !enemy.WallDetected && !enemy.CliffDetected;
}
