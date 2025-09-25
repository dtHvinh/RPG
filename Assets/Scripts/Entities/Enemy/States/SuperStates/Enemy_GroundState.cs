public class Enemy_GroundState : EnemyState
{
    public Enemy_GroundState(EntityStateMachine stateMachine, Enemy entity, string animationBoolName)
        : base(stateMachine, entity, animationBoolName)
    {
    }

    public override void Update()
    {
        base.Update();

        if (enemy.DetectTarget() == true
            && enemy.AI.ShouldStartChasingTarget())
        {
            stateMachine.ChangeState(enemy.BattleState);
        }
    }
}
