public class Enemy_MoveState : Enemy_GroundState
{
    public Enemy_MoveState(EntityStateMachine stateMachine, EnemyBase entity, string animationBoolName)
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
