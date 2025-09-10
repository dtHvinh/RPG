using System;
using UnityEngine;

public class EntityStats : MonoBehaviour
{
    [SerializeField] private EntityStatsSO baseStats;

    [SerializeField] private Stat currentHealth;

    [SerializeField] private Stat attackRadius;
    [SerializeField] private Stat physicalDamage;
    [SerializeField] private Stat fireDamage;
    [SerializeField] private Stat iceDamage;
    [SerializeField] private Stat lightningDamage;
    [SerializeField] private Stat voidDamage;
    [SerializeField] private Stat earthDamage;
    [SerializeField] private Stat trueDamage;


    public event EventHandler<DamageTakenEventArgs> OnDamageTaken;
    public event EventHandler<EntityDeathEventArgs> OnDeath;

    private void Awake()
    {
        currentHealth = baseStats.maxHealth;
        attackRadius = baseStats.attackRadius;

        physicalDamage = baseStats.physicalDamage;
        fireDamage = baseStats.fireDamage;
        iceDamage = baseStats.iceDamage;
        lightningDamage = baseStats.lightningDamage;
        voidDamage = baseStats.voidDamage;
        earthDamage = baseStats.earthDamage;
        trueDamage = baseStats.trueDamage;
    }

    public float GetCurrentHealth() => currentHealth.Value;

    public float GetMaxHealth() => baseStats.maxHealth;

    public float GetAttackRadius() => attackRadius.Value;

    public float GetPhysicalDamage() => physicalDamage.Value;

    public void TakeDamage(DameDealingInfo damage)
    {
        float totalDamage = damage.physicalDamage;
        currentHealth = Mathf.Clamp(GetCurrentHealth() - totalDamage, 0, baseStats.maxHealth);

        if (IsDead())
        {
            OnDeath?.Invoke(this, EntityDeathEventArgs.Create());
        }

        OnDamageTaken?.Invoke(this, DamageTakenEventArgs.Create(totalDamage));
    }

    public bool IsDead() => currentHealth.Value <= 0;

    [Serializable]
    public struct Stat
    {
        public float baseValue;

        public readonly float Value => baseValue;

        public Stat(float baseValue) => this.baseValue = baseValue;

        public static implicit operator Stat(float value) => new(value);
    }

    public class DamageTakenEventArgs : EventArgs
    {
        public float damageAmount;

        public static DamageTakenEventArgs Create(float damageAmount)
        {
            return new DamageTakenEventArgs { damageAmount = damageAmount };
        }
    }

    public class EntityDeathEventArgs : EventArgs
    {
        public static EntityDeathEventArgs Create()
        {
            return new EntityDeathEventArgs();
        }
    }
}
