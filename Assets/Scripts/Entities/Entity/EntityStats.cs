using UnityEngine;

public class EntityStats : MonoBehaviour, IStats
{
    [SerializeField] private EntityBaseStatsSO baseStats;

    [field: SerializeField, Header("Primary stats")] public Stat BaseMaxHealth { get; private set; }
    [field: SerializeField] public Stat AttackRadius { get; private set; }
    [field: SerializeField] public Stat MoveSpeed { get; private set; }
    [field: SerializeField] public Stat JumpForce { get; private set; }
    public Vector2 WallJumpForce { get; private set; }

    [field: SerializeField, Header("Core stats")] public Stat Vitality { get; private set; }
    [field: SerializeField] public Stat Strength { get; private set; }
    [field: SerializeField] public Stat Agility { get; private set; }
    [field: SerializeField] public Stat Intelligence { get; private set; }

    [field: SerializeField, Header("Offensive stats")] public Stat Attack { get; private set; }
    [field: SerializeField] public Stat CriticalChance { get; private set; }
    [field: SerializeField] public Stat CriticalDamage { get; private set; }
    [field: SerializeField] public Stat MagicDamage { get; private set; }

    [field: SerializeField, Header("Defensive stats")] public Stat Evasion { get; private set; }
    [field: SerializeField] public Stat Armor { get; private set; }
    [field: SerializeField] public Stat MagicResistant { get; private set; }

    [field: SerializeField, Header("Reduction stats")] public Stat ArmorPenetration { get; private set; }

    [field: SerializeField, Header("Resources")] public Stat HealthRegen { get; private set; }
    [field: SerializeField] public Stat HealthInterval { get; private set; }

    private void Awake()
    {
        BaseMaxHealth = baseStats.maxHealth;
        AttackRadius = baseStats.attackRadius;
        MoveSpeed = baseStats.moveSpeed;
        JumpForce = baseStats.jumpForce;
        WallJumpForce = baseStats.wallJumpForce;

        Vitality = baseStats.vitality;
        Strength = baseStats.strength;
        Agility = baseStats.agility;
        Intelligence = baseStats.intelligence;

        Attack = baseStats.baseAttack;
        CriticalChance = baseStats.criticalChance;
        CriticalDamage = baseStats.criticalDamage;
        MagicDamage = baseStats.magicDamage;

        Evasion = baseStats.evasion;
        Armor = baseStats.armor;
        MagicResistant = baseStats.magicResistant;

        ArmorPenetration = baseStats.armorPenetration;

        HealthRegen = baseStats.healthRegen;
        HealthInterval = baseStats.healthInterval;
    }

    public float GetMaxHealth()
    {
        return BaseMaxHealth.Value + (Vitality.Value * StatFormular.VIT_HEALTH_CONVERT_RATE);
    }

    public MigrationResult GetArmorMitigation()
    {
        float baseArmor = Armor.Value;
        float bonusArmor = Vitality.Value * StatFormular.VIT_ARMOR_CONVERT_RATE;
        float totalArmor = baseArmor + bonusArmor;

        return new MigrationResult(
            totalArmor: totalArmor);
    }

    public ArmorPenetrationResult GetArmorPenetration()
    {
        return new ArmorPenetrationResult(
            totalReduction: ArmorPenetration.Value / StatFormular.ARMOR_PEN_SCALING_CONSTANT);
    }

    public DameResult GetPhysicalDamage()
    {
        float baseAttack = Attack;
        float strengthBonus = Strength * StatFormular.STR_ATK_CONVERT_RATE;

        float baseCriticalChance = CriticalChance;
        float bonusCriticalChance = Agility * StatFormular.AGLI_CRIT_CONVERT_RATE;

        bool isCritical = Random.value < baseCriticalChance + bonusCriticalChance;

        return new DameResult(
            dameType: DameTypes.Physical,
            baseDamage: baseAttack + strengthBonus,
            isCritical: isCritical,
            multiplier: CriticalDamage);
    }

    public DameResult GetElementalDamage()
    {
        float baseDamage = MagicDamage;

        float bonusDamage = Intelligence * StatFormular.STR_ATK_CONVERT_RATE;

        return new DameResult(
            dameType: DameTypes.Magical,
            baseDamage: baseDamage + bonusDamage,
            isCritical: false,
            multiplier: 1);
    }

    public ElementalResistanceResult GetMagicalResistant()
    {
        float baseResistance = 0f;

        float bonusResistance = Intelligence.Value * StatFormular.INTEL_ELEMENTAL_RES_CONVERT_RATE;

        return new ElementalResistanceResult(
            totalResistance: baseResistance + bonusResistance);
    }
}
