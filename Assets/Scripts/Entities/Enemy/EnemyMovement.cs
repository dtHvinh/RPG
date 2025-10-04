using UnityEngine;

[RequireComponent(typeof(IStats))]
public class EnemyMovement : EntityMovement
{
    private IStats stats;

    [Header("Enemy Movement Settings")]
    [Range(0, 2)] public float BattleAnimSpeedMulti = 1f;
    public float IdleTime = 2f;

    protected override void Awake()
    {
        base.Awake();

        stats = GetComponent<IStats>();
    }

    public void MoveWithBaseSpeed()
    {
        SetVelocity(stats.MoveSpeed * FacingDirection, entity.Rb.linearVelocityY);
    }
}
