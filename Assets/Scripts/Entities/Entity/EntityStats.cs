using UnityEngine;

public class EntityStats : MonoBehaviour, IStats
{
    [SerializeField] private EntityBaseStatsSO baseStats;

    [Header("Primary stats")]
    public Stat BaseMaxHealth { get; private set; }
    public Stat AttackRadius { get; private set; }
    public Stat MoveSpeed { get; private set; }
    public Stat JumpForce { get; private set; }
    public Vector2 WallJumpForce { get; private set; }

    [Header("Core stats")]
    public Stat Vitality { get; private set; }
    public Stat Strength { get; private set; }
    public Stat Agility { get; private set; }
    public Stat Intelligence { get; private set; }

    [Header("Offensive stats")]
    public Stat Attack { get; private set; }
    public Stat CriticalChance { get; private set; }
    public Stat CriticalDamage { get; private set; }
    public Stat IceDamage { get; private set; }
    public Stat FireDamage { get; private set; }
    public Stat LightningDamage { get; private set; }

    [Header("Defensive stats")]
    public Stat Evasion { get; private set; }
    public Stat Armor { get; private set; }
    public Stat FireResistant { get; private set; }
    public Stat IceResistant { get; private set; }
    public Stat LightningResistant { get; private set; }

    [Header("Reduction stats")]
    public Stat ArmorPenetration { get; private set; }

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
        IceDamage = baseStats.iceDamage;
        FireDamage = baseStats.fireDamage;
        LightningDamage = baseStats.lightningDamage;

        Evasion = baseStats.evasion;
        Armor = baseStats.armor;
        FireResistant = baseStats.fireResistance;
        IceResistant = baseStats.iceResistance;
        LightningResistant = baseStats.lightningResistance;

        ArmorPenetration = baseStats.armorPenetration;
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

    public ArmorReductionResult GetArmorPenetration()
    {
        return new ArmorReductionResult(
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
        float iceDamage = IceDamage;
        float fireDamage = FireDamage;
        float lightningDamage = LightningDamage;

        float highestEleDamage = Mathf.Max(iceDamage, fireDamage, lightningDamage);

        if (highestEleDamage == 0)
            return new DameResult(DameTypes.None, 0, false, 1);
        DameTypes dameType = DameTypes.Ice;

        if (highestEleDamage == fireDamage)
            dameType = DameTypes.Fire;

        else if (highestEleDamage == lightningDamage)
            dameType = DameTypes.Lightning;

        float bonusDamage = Intelligence * StatFormular.STR_ATK_CONVERT_RATE;

        // Final elemental damage is calculated by taking the highest elemental damage add half
        // of the sum of the other two, plus bonus damage from intelligence
        float finalDamage = highestEleDamage + (iceDamage + fireDamage + lightningDamage - highestEleDamage) * 0.5f + bonusDamage;

        return new DameResult(
            dameType: dameType,
            baseDamage: finalDamage,
            isCritical: false,
            multiplier: 1);
    }

    public ElementalResistanceResult GetElementalResistance(DameTypes dameType)
    {
        float baseResistance = 0f;
        switch (dameType)
        {
            case DameTypes.Fire:
                baseResistance = FireResistant;
                break;
            case DameTypes.Ice:
                baseResistance = IceResistant;
                break;
            case DameTypes.Lightning:
                baseResistance = LightningResistant;
                break;
            default:
                return new ElementalResistanceResult(0);
        }

        float bonusResistance = Intelligence.Value * StatFormular.INTEL_ELEMENTAL_RES_CONVERT_RATE;

        return new ElementalResistanceResult(
            totalResistance: baseResistance + bonusResistance);
    }
}
