public readonly struct MigrationResult
{
    public readonly float TotalArmor;

    public MigrationResult(float totalArmor) => TotalArmor = totalArmor;
}

public readonly struct DameResult
{
    public readonly DameTypes DameType;
    public readonly float BaseDamage;
    public readonly bool IsCritical;
    public readonly float CriticalDamageMultiplier;

    public DameResult(DameTypes dameType, float baseDamage, bool isCritical, float multiplier)
    {
        DameType = dameType;
        BaseDamage = baseDamage;
        IsCritical = isCritical;
        CriticalDamageMultiplier = multiplier;
    }
}

public readonly struct ArmorPenetrationResult
{
    public readonly float TotalReduction;

    public static ArmorPenetrationResult Zero => new(0f);

    public ArmorPenetrationResult(float totalReduction) => TotalReduction = totalReduction;
}

public readonly struct ElementalResistanceResult
{
    public readonly float TotalResistance;

    public static ElementalResistanceResult Zero => new(0f);

    public ElementalResistanceResult(float totalResistance) => TotalResistance = totalResistance;
}