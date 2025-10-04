public class Enemy_AttackState : EnemyState
{
    public Enemy_AttackState(EntityStateMachine stateMachine, Enemy enemy, string animationBoolName)
        : base(stateMachine, enemy, animationBoolName)
    {
    }

    public override void Enter()
    {
        enemy.Movement.Stop();
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        if (triggerCalled)
            stateMachine.ChangeState(enemy.BattleState);
    }
}
