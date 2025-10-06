using UnityEngine;

public interface IStats
{
    Stat BaseMaxHealth { get; }
    Stat AttackRadius { get; }
    Stat MoveSpeed { get; }
    Stat JumpForce { get; }
    Vector2 WallJumpForce { get; }
    Stat Vitality { get; }
    Stat Strength { get; }
    Stat Agility { get; }
    Stat Intelligence { get; }
    Stat Attack { get; }
    Stat CriticalChance { get; }
    Stat CriticalDamage { get; }
    Stat Evasion { get; }
    Stat Armor { get; }
    Stat MagicResistant { get; }
    Stat ArmorPenetration { get; }
    Stat HealthRegen { get; }
    Stat HealthInterval { get; }

    MigrationResult GetArmorMitigation();
    ArmorPenetrationResult GetArmorPenetration();
    float GetMaxHealth();
    DameResult GetPhysicalDamage();
}

