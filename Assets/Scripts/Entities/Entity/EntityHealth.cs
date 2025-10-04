using System;
using UnityEngine;
using static EntityHurtEventArgs;

[RequireComponent(typeof(EntityStats), typeof(Entity))]
public class EntityHealth : MonoBehaviour, IHealth
{
    private EntityStats stats;
    private Entity entity;

    [ReadOnly] public float MaxHealth;
    [ReadOnly] public float CurrentHealth;
    private bool isDead = false;

    public event EventHandler<EntityHurtEventArgs> OnHurt;
    public event EventHandler<DeathEventArgrs> OnDeath;
    public event EventHandler<HealthChangeEventArgs> OnHealthChange;

    public virtual void Awake()
    {
        stats = GetComponent<EntityStats>();
        entity = GetComponent<Entity>();
    }

    public virtual void Start()
    {
        MaxHealth = stats.GetMaxHealth();
        CurrentHealth = stats.GetMaxHealth();
    }

    public virtual void TakeDamage(DameInstance info)
    {
        if (isDead)
            return;

        float finalDamage = StatFormular.FinalDameTaken(info.damage, stats.GetArmorMitigation(), info.armorReduction);
        CurrentHealth = Mathf.Clamp(CurrentHealth - finalDamage, 0, MaxHealth);

        OnHealthChange?.Invoke(this, (CurrentHealth, MaxHealth));
        OnHurt?.Invoke(this, new HurtInstance()
        {
            DamageType = info.damage.DameType,
            IsCritical = info.damage.IsCritical,
            DameDealer = info.dameDealer,
            FinalDamage = finalDamage,
        });

        if (CurrentHealth <= 0)
        {
            OnDeath?.Invoke(this, (info.dameDealer, entity));
            isDead = true;
        }
    }
}

#region EventArgs

#endregion
