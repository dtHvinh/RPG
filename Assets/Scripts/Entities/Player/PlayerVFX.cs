using UnityEngine;

public class PlayerVFX : EntityVFX
{
    private Player player;
    [SerializeField] private SpawnParams dameHitParams;

    public override void Awake()
    {
        base.Awake();

        player = GetComponentInParent<Player>();
    }

    private void Start()
    {
        player.Combat.OnPerformAttack += Combat_OnPerformAttack;
        player.Health.OnHurt += Health_OnHurt;
    }

    private void Health_OnHurt(object sender, EntityHurtEventArgs e)
    {
        VFXManager.Instance.ShowDamagePopup(player.Transform.position, e.Hurt, dameHitParams);
    }

    private void Combat_OnPerformAttack(object sender, EntityCombat.PerformAttackEventArgs e)
    {
        VFXManager.Instance.ShowHit(e.attackOrigin.position, hitColor, SpawnParams.Create(.5f, .5f));
    }
}
