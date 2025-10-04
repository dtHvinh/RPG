using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class EnemyVFX : EntityVFX
{
    private ICounterable counterable;
    private Enemy enemy;
    [SerializeField] private SpawnParams dameHitParams;

    [Header("Counter Attack Window")]
    [SerializeField] private GameObject attackAlert;

    public override void Awake()
    {
        base.Awake();

        counterable = GetComponentInParent<ICounterable>();
        enemy = GetComponentInParent<Enemy>();
    }

    private void Start()
    {
        if (counterable != null)
        {
            counterable.OnCounter += Counterable_OnCounter;
        }

        if (enemy != null)
        {
            enemy.Combat.OnPerformAttack += Combat_OnPerformAttack;
            enemy.Health.OnHurt += Health_OnHurt;
        }
    }

    private void Health_OnHurt(object sender, EntityHurtEventArgs e)
    {
        VFXManager.Instance.ShowDamagePopup(enemy.Transform.position, e.Hurt, dameHitParams);
    }

    private void Combat_OnPerformAttack(object sender, EntityCombat.PerformAttackEventArgs e)
    {
        VFXManager.Instance.ShowHit(e.attackOrigin.position, hitColor, SpawnParams.Create(.5f, .5f));
    }

    private void Counterable_OnCounter(object sender, System.EventArgs e)
    {
        EnableAttackAlert(false);
    }

    public void EnableAttackAlert(bool enable)
    {
        if (attackAlert != null)
            attackAlert.SetActive(enable);
    }
}
