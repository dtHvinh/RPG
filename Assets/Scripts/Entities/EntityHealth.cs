using UnityEngine;

public class EntityHealth : MonoBehaviour
{
    [ReadOnly] public float maxHealth = 100f;
    [ReadOnly] public bool isDead = false;

    public virtual void TakeDamage(DameDealingInfo info)
    {
        if (isDead)
            return;

        ReduceHp(info.damage);
    }

    private void ReduceHp(float dame)
    {
        maxHealth -= dame;

        if (maxHealth <= 0 && !isDead)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
    }
}
