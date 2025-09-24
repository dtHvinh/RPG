public class Enemy_IdleState : Enemy_GroundState
{
    public Enemy_IdleState(EntityStateMachine stateMachine, EnemyBase entity, string animationBoolName)
        : base(stateMachine, entity, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = enemy.IdleTime;

        enemy.SetVelocity(0f, Rb.linearVelocityY);
    }

    public override void Update()
    {
        base.Update();

        if (enemy.CanMove())
        {
            stateMachine.ChangeState(enemy.MoveState);
        }
        else if (stateTimer <= 0f)
        {
            enemy.Flip();
        }
    }
}
