public class EnemyAI
{
    private readonly EnemyBase enemy;

    public EnemyAI(EnemyBase enemy)
    {
        this.enemy = enemy;
    }

    public bool ShouldKeepChasingTarget()
    {
        return !enemy.WallDetected && !enemy.CliffDetected;
    }

    public bool ShouldStartChasingTarget()
    {
        return !enemy.CliffDetected;
    }
}
