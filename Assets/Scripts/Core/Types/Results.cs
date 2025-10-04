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

public readonly struct ArmorReductionResult
{
    public readonly float TotalReduction;

    public ArmorReductionResult(float totalReduction) => TotalReduction = totalReduction;
}

public readonly struct ElementalResistanceResult
{
    public readonly float TotalResistance;
    public ElementalResistanceResult(float totalResistance) => TotalResistance = totalResistance;
}