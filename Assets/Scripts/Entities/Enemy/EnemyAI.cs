public class EnemyAI
{
    private readonly Enemy enemy;

    public EnemyAI(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public bool ShouldKeepChasingTarget()
    {
        return true;
        //return enemy.FallDameSystem.CheckSafeFall(enemy.Collision.CliffCheckPoint);
    }

    public bool ShouldStartChasingTarget()
    {
        if (enemy.Collision.CliffDetected)
        {
            return true;
            //return enemy.FallDameSystem.CheckSafeFall(enemy.Collision.CliffCheckPoint);
        }

        return true;
    }
}
