using UnityEngine;


[CreateAssetMenu(fileName = "EntityStatsSO", menuName = "RPG/Entity Stats", order = 1)]
public class EntityStatsSO : ScriptableObject
{
    [Header("Offensive stats")]
    public float attackRadius = 1f;

    [Header("Defensive stats")]
    public float maxHealth = 100f;

    [Header("Movement stats")]
    public float moveSpeed = 8f;
    public float jumpForce = 12f;
    public Vector2 wallJumpForce;

    [Header("Damage stats")]
    public SerializableDictionary<DamageType, float> damageStats;
}