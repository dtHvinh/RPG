public class Enemy_IdleState : Enemy_GroundState
{
    public Enemy_IdleState(EntityStateMachine stateMachine, Enemy enemy, string animationBoolName)
        : base(stateMachine, enemy, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        SetTimer(enemy.Movement.IdleTime);

        enemy.SetVelocity(0f, Rb.linearVelocityY);
    }

    public override void Update()
    {
        base.Update();

        if (!enemy.Collision.WallDetected && !enemy.Collision.CliffDetected)
        {
            stateMachine.ChangeState(enemy.MoveState);
        }
        else if (IsTimerFinished())
        {
            enemy.Movement.Flip();
        }
    }
}
