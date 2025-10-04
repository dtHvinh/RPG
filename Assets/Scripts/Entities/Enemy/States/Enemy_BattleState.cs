public class Enemy_BattleState : EnemyState
{
    public Enemy_BattleState(EntityStateMachine stateMachine, Enemy enemy, string animationBoolName)
        : base(stateMachine, enemy, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        enemy.Stats.MoveSpeed
            .AddModifier(
                StatModifier.Create(typeof(Enemy_BattleState), enemy.Movement.BattleAnimSpeedMulti, StatModifierType.PercentAdd));

        if (enemy.Combat.GetTarget() == null)
            enemy.Combat.SetTarget(enemy.Combat.DetectTarget().transform);
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
            {
                HandleFlip();

                enemy.SetVelocity(
                    enemy.Stats.MoveSpeed * enemy.Combat.DirectionToTarget(),
                    enemy.Rb.linearVelocityY);
            }
            else
            {
                enemy.Combat.ClearTarget();
                stateMachine.ChangeState(enemy.IdleState);
            }
        }
    }

    private void HandleFlip()
    {
        if (enemy.Rb.linearVelocityX < 0 && enemy.FacingDirection > 0
        || enemy.Rb.linearVelocityX > 0 && enemy.FacingDirection < 0)
        {
            enemy.Movement.Flip();
        }
    }

    public override void Exit()
    {
        base.Exit();

        enemy.Stats.MoveSpeed.RemoveModifiersFromSource(GetType());
    }
}
