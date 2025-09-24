public class Enemy_AttackState : EnemyState
{
    public Enemy_AttackState(EntityStateMachine stateMachine, EnemyBase entity, string animationBoolName)
        : base(stateMachine, entity, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        entity.StopMovementX();
    }

    public override void Update()
    {
        base.Update();

        if (triggerCalled)
            stateMachine.ChangeState(enemy.BattleState);
    }
}
