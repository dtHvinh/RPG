public class PlayerAnimationTrigger : EntityAnimationTriggers
{
    private Player player;

    protected override void Awake()
    {
        player = GetComponentInParent<Player>();
        entity = player;
    }

    protected void AttackTrigger()
    {
        player.Combat.PerformAttack();
    }
}
