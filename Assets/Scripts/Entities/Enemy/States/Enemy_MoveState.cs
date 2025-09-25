public class Enemy_MoveState : Enemy_GroundState
{
    public Enemy_MoveState(EntityStateMachine stateMachine, Enemy enemy, string animationBoolName)
        : base(stateMachine, enemy, animationBoolName)
    {
    }

    public override void Update()
    {
        base.Update();

        if (!enemy.Collision.WallDetected && !enemy.Collision.CliffDetected)
        {
            enemy.Movement.MoveWithBaseSpeed();
        }
        else
        {
            stateMachine.ChangeState(enemy.IdleState);
        }
    }
}
