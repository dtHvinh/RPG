using System.Collections.Generic;
using UnityEngine;

public static class StatFormular
{
    // Scaling factors
    public const int ARMOR_SCALING_CONSTANT = 100;
    public const int ARMOR_PEN_SCALING_CONSTANT = 100;

    // Conversion rates
    public const float VIT_HEALTH_CONVERT_RATE = 5f;
    public const float VIT_ARMOR_CONVERT_RATE = 5f;

    public const float STR_ATK_CONVERT_RATE = 5f;

    public const float AGLI_CRIT_CONVERT_RATE = 0.04f;

    public const float INTEL_ELEMENTAL_DMG_CONVERT_RATE = 1f;
    public const float INTEL_ELEMENTAL_RES_CONVERT_RATE = .5f;

    // Caps
    public const float MAX_MIGRATION = 0.75f;
    public const float MAX_MAGIC_MIGRATION = 0.75f;

    public static float GetArmorMigration(float armor)
    {
        return Mathf.Clamp(armor / (armor + ARMOR_SCALING_CONSTANT), 0, MAX_MIGRATION);
    }

    public static float GetMagicMigration(float resistance)
    {
        return Mathf.Clamp(resistance / (resistance + ARMOR_SCALING_CONSTANT), 0, MAX_MAGIC_MIGRATION);
    }

    public static float FinalDameTaken(DameResult dameResult,
                                       MigrationResult migrationResult,
                                       ArmorPenetrationResult armorReductionResult)
    {
        if (dameResult.DameType == DameTypes.TrueDame)
            return dameResult.BaseDamage * (dameResult.IsCritical ? dameResult.CriticalDamageMultiplier : 1);

        float rawDamage = dameResult.BaseDamage * (dameResult.IsCritical ? dameResult.CriticalDamageMultiplier : 1);

        float armor = migrationResult.TotalArmor * (1 - armorReductionResult.TotalReduction);

        float dameAfterMigrationMultiplier = 1 - GetArmorMigration(armor);

        return rawDamage * dameAfterMigrationMultiplier;
    }

    public static float FinalStat(float baseStat, List<StatModifier> modifiers)
    {
        float flatAdd = 0f;
        float percentAdd = 0f;
        float percentMult = 0f;

        foreach (var mod in modifiers)
        {
            switch (mod.Type)
            {
                case StatModifierType.Flat:
                    flatAdd += mod.Value;
                    break;
                case StatModifierType.PercentAdd:
                    percentAdd += mod.Value;
                    break;
                case StatModifierType.PercentMult:
                    percentMult += mod.Value;
                    break;
            }
        }

        float finalValue = baseStat + (baseStat * percentAdd) + flatAdd;

        finalValue *= 1 + percentMult;

        return finalValue;
    }

}
