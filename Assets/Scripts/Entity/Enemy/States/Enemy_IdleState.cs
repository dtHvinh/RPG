using UnityEngine;

public class Enemy_IdleState : EnemyState
{
    public Enemy_IdleState(EntityStateMachine stateMachine, Enemy entity, string animationBoolName) 
        : base(stateMachine, entity, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        enemy.SetVelocity(0f, Rb.linearVelocityY);
    }

    public override void Update()
    {
        base.Update();

        if(!enemy.WallDetected)
        {
            stateMachine.ChangeState(enemy.MoveState);
        }
    }
}
