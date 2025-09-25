public class Enemy_BattleState : EnemyState
{
    public Enemy_BattleState(EntityStateMachine stateMachine, Enemy enemy, string animationBoolName)
        : base(stateMachine, enemy, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        enemy.Stats.MoveSpeed.AddPercentModifier(enemy.BattleAnimSpeedMulti);

        enemy.Combat.SetTarget(enemy.DetectTarget().transform);
    }

    public override void Update()
    {
        base.Update();

        if (enemy.Combat.GetTarget() == null)
        {
            stateMachine.ChangeState(enemy.IdleState);
            return;
        }

        if (enemy.Combat.WithinAttackDistance())
        {
            enemy.Combat.FacingToTarget();
            stateMachine.ChangeState(enemy.AttackState);
        }
        else
        {
            if (enemy.AI.ShouldKeepChasingTarget())
                enemy.SetVelocity(
                    enemy.Stats.MoveSpeed * enemy.Combat.DirectionToTarget(),
                    enemy.Rb.linearVelocityY);
            else
            {
                enemy.Combat.ClearTarget();
                stateMachine.ChangeState(enemy.IdleState);
            }
        }
    }

    public override void Exit()
    {
        base.Exit();

        enemy.Stats.MoveSpeed.ClearModifiers();
    }
}
