using System.Collections.Generic;
using UnityEngine;

public enum StatType
{
    Health,
    MoveSpeed,
    JumpForce
}

public enum DamageType
{
    Physical,
    Fire,
    Ice,
    Lightning,
    Void,
    Earth,
    True
}

public class EntityStats : MonoBehaviour
{
    [SerializeField] private EntityStatsSO baseStats;

    [SerializeField] private Dictionary<DamageType, Stat> damageValues;

    public Stat CurrentHealth { get; private set; }
    public Stat AttackRadius { get; private set; }
    public Stat MoveSpeed { get; private set; }
    public Stat JumpForce { get; private set; }
    public Vector2 WallJumpForce { get; private set; }

    private void Awake()
    {
        damageValues = new Dictionary<DamageType, Stat>();

        CurrentHealth = baseStats.maxHealth;

        //foreach (var keyVal in baseStats.damageStats.ToDictionary())
        //{
        //    damageValues.Add(keyVal.Key, keyVal.Value);
        //}

        AttackRadius = baseStats.attackRadius;
        MoveSpeed = baseStats.moveSpeed;
        JumpForce = baseStats.jumpForce;
        WallJumpForce = baseStats.wallJumpForce;
    }
}
