using UnityEngine;

[CreateAssetMenu(fileName = "EntityStatsSO", menuName = "RPG/Entity Stats", order = 1)]
public class EntityStatsSO : ScriptableObject
{
    [Header("Defensive stats")]
    public float maxHealth = 100f;

    [Header("Offensive stats")]
    public float attackRadius = .75f;

    [Header("Damage stats")]
    public float physicalDamage = 10f;
    public float fireDamage;
    public float iceDamage;
    public float lightningDamage;
    public float voidDamage;
    public float earthDamage;
    public float trueDamage;
}
