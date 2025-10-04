using UnityEngine;

public interface ICombat
{
    void ApplyKnockback(float direction);
    float CalculateKnockbackDirection(Transform source);
    void ClearTarget();
    HitBox[] GetDetectedHitBoxes(float attackRadius);
    int GetDetectedHitBoxes(float attackRadius, HitBox[] hitBoxes);
    Transform GetTarget();
    void SetTarget(Transform target);
    void SwitchTarget(Transform target);
}
