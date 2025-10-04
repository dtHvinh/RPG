public class Enemy_HurtState : EnemyState
{
    public Enemy_HurtState(EntityStateMachine stateMachine, Enemy enemy, string animationBoolName)
        : base(stateMachine, enemy, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        enemy.Movement.Stop();
        enemy.Combat.ApplyKnockback(enemy.Combat.CalculateKnockbackDirection(enemy.Combat.GetTarget()));
    }

    public override void Update()
    {
        base.Update();

        if (triggerCalled)
            stateMachine.ChangeState(enemy.BattleState);
    }
}
