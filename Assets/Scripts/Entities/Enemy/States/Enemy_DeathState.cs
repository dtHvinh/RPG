public class Enemy_DeathState : EnemyState
{
    public Enemy_DeathState(EntityStateMachine stateMachine, Enemy enemy, string animationBoolName)
        : base(stateMachine, enemy, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        enemy.Movement.Stop();

        UnityEngine.Object.Destroy(enemy.gameObject, 2f);
    }
}
