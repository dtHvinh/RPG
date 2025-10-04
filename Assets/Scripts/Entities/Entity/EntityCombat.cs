using System;
using System.Collections;
using UnityEngine;
/**
* The colliders field cache the results of the OverlapCircle call. It not necessary show the collider hit when attack, it may still
* store some collider the old results but use it will the count that method return which show the index that method actually hit.
* 
*/

[RequireComponent(typeof(IMovement))]
[RequireComponent(typeof(IEntity))]
[RequireComponent(typeof(IStats))]
public abstract class EntityCombat : MonoBehaviour, ICombat
{
    protected IMovement movement;
    protected Transform targetTransform;
    protected IStats stats;
    protected IEntity entity;


    private Collider2D[] colliders;
    [SerializeField, Tooltip("Max ammount of colliders can hit")] private int collidersCount = 5;
    private HitBox[] hitBoxes;
    [SerializeField, Tooltip("Max ammount of hitbox can hit")] private int hitBoxCount = 5;
    private ContactFilter2D targetContactFilter;

    [Header("Knockback")]
    [SerializeField] private Vector2 knockbackStrength;
    [SerializeField] private float knockbackDuration = 0.2f;
    private Coroutine knockbackCo;
    public bool IsKnockbacking { get; private set; }

    [Header("Target detection")]
    [SerializeField] protected Transform attackPoint;
    [SerializeField] protected LayerMask targetLayers;

    public event EventHandler<PerformAttackEventArgs> OnPerformAttack;
    public event EventHandler OnDealCriticalHit;

    protected virtual void Awake()
    {
        movement = GetComponent<IMovement>();
        stats = GetComponent<IStats>();
        entity = GetComponent<IEntity>();

        colliders = new Collider2D[collidersCount];
        hitBoxes = new HitBox[hitBoxCount];
        targetContactFilter = new ContactFilter2D();
        targetContactFilter.SetLayerMask(targetLayers);
    }

    public HitBox[] GetDetectedHitBoxes(float attackRadius)
    {
        int colliderCount = Physics2D.OverlapCircle(attackPoint.position, attackRadius, targetContactFilter, colliders);

        HitBox[] hitBoxes = new HitBox[colliderCount];
        for (int i = 0; i < colliderCount; i++)
        {
            colliders[i].TryGetComponent(out hitBoxes[i]);
        }

        return hitBoxes;
    }

    public int GetDetectedHitBoxes(float attackRadius, HitBox[] hitBoxes)
    {
        int colliderCount = Physics2D.OverlapCircle(attackPoint.position, attackRadius, targetContactFilter, colliders);

        for (int i = 0; i < colliderCount; i++)
        {
            colliders[i].TryGetComponent(out hitBoxes[i]);
        }

        return colliderCount;
    }

    public int GetHitBoxes(float attackRadius)
    {
        return GetDetectedHitBoxes(attackRadius, hitBoxes);
    }

    public HitBox[] GetHitBoxes() => hitBoxes;

    public void SetTarget(Transform target)
    {
        if (target != null && targetTransform == null)
        {
            targetTransform = target;
        }
    }

    public void SwitchTarget(Transform target)
    {
        if (target != null)
        {
            targetTransform = target;
        }
    }

    public Transform GetTarget() => targetTransform;

    public void ClearTarget() => targetTransform = null;

    public float GetAttackDistance() => transform.DistanceXTo(attackPoint.transform);

    public bool WithinAttackDistance(Transform target) => transform.DistanceTo(target) <= GetAttackDistance();

    public bool WithinAttackDistance() => targetTransform != null && WithinAttackDistance(targetTransform);

    public void InvokeOnPerformAttack(PerformAttackEventArgs e)
    {
        OnPerformAttack?.Invoke(this, e);
    }

    public void InvokeOnDealCriticalHit()
    {
        OnDealCriticalHit?.Invoke(this, EventArgs.Empty);
    }

    public virtual void PerformAttack()
    {
        int count = GetHitBoxes(stats.AttackRadius);

        for (int i = 0; i < count; i++)
        {
            HitBox hitBox = GetHitBoxes()[i];
            IHealth health = hitBox.GetHealth();

            if (health != null)
            {
                DameInstance dameInstance = new(stats.GetPhysicalDamage(), stats.GetArmorPenetration(), entity);
                health.TakeDamage(dameInstance);

                InvokeOnPerformAttack(hitBox.transform);

                if (dameInstance.damage.IsCritical)
                {
                    InvokeOnDealCriticalHit();
                }
            }
        }
    }

    public void ApplyKnockback(float direction)
    {
        Vector2 knockback = new(knockbackStrength.x * direction, knockbackStrength.y);

        if (knockbackCo != null)
            StopCoroutine(knockbackCo);
        knockbackCo = StartCoroutine(KnockbackCoroutine(knockback, knockbackDuration));
    }

    public float CalculateKnockbackDirection(Transform source)
    {
        return transform.position.x - source.position.x >= 0 ? 1 : -1;
    }

    private IEnumerator KnockbackCoroutine(Vector2 knockbackVelocity, float duration)
    {
        IsKnockbacking = true;
        movement.SetVelocity(knockbackVelocity);
        yield return new WaitForSeconds(duration);
        movement.SetVelocity(Vector2.zero);
        IsKnockbacking = false;
    }

    public class PerformAttackEventArgs : EventArgs
    {
        public Transform attackOrigin;

        public static implicit operator PerformAttackEventArgs(Transform v)
        {
            return new PerformAttackEventArgs() { attackOrigin = v };
        }
    }
}
