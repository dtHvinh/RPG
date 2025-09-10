using UnityEngine;

public class Entity : MonoBehaviour, IDamageable
{
    protected Rigidbody2D rb;
    protected Animator animator;
    protected Collider2D entityCollider;

    [Header("Movement Details")]
    [SerializeField] protected float moveSpeed = 8f;
    [SerializeField] protected float jumpForce = 12f;
    protected float facingDirection = 1;
    protected bool facingRight = true;
    protected bool canMove = true;
    protected bool canJump = true;

    [Header("Collition Details")]
    private const float groundCheckDistance = 1.5f;
    protected bool isGrounded = true;
    [SerializeField] private LayerMask groundLayer;

    [Header("Attack Details")]
    [SerializeField] protected Transform attackPoint;
    [SerializeField] protected LayerMask targetLayers;

    protected EntityStats stats;

    public void Start()
    {
        stats = GetComponent<EntityStats>();
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        entityCollider = GetComponent<Collider2D>();

        stats.OnDeath += Stats_OnDeath;
    }

    private void Stats_OnDeath(object sender, EntityStats.EntityDeathEventArgs e)
    {
        Die();
    }

    protected virtual void Update()
    {
        HandleMovement();
        HandleAnimations();
        HandleFlip();
        HandleCollision();
        HandleAttack();
    }

    // Movement
    public void SetCanMove(bool value) => canMove = value;

    public void SetCanJump(bool value) => canJump = value;

    protected virtual void HandleMovement()
    {

    }

    protected virtual void HandleAnimations()
    {
        animator.SetBool(AnimatorConstants.IS_GROUNDED, isGrounded);
        animator.SetFloat(AnimatorConstants.Y_VELOCITY, rb.linearVelocity.y);
        animator.SetFloat(AnimatorConstants.X_VELOCITY, rb.linearVelocity.x);
    }

    protected void HandleFlip()
    {
        if (rb.linearVelocity.x > 0 && !facingRight || rb.linearVelocity.x < 0 && facingRight)
        {
            transform.Rotate(0f, 180f, 0f);
            facingRight = !facingRight;
            facingDirection *= -1;
        }
    }

    protected bool TryToJump()
    {
        if (isGrounded && canJump)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            return true;
        }

        return false;
    }

    // Collisions
    protected virtual void HandleCollision()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer);
    }

    // Attacking
    protected virtual DameDealingInfo CreateDamageDealingInfo()
    {
        return new DameDealingInfo
        {
            physicalDamage = stats.GetPhysicalDamage(),
            earthDamage = 0,
            fireDamage = 0,
            iceDamage = 0,
            lightningDamage = 0,
            voidDamage = 0,
            trueDamage = 0
        };
    }

    protected virtual void HandleAttack()
    {
        if (isGrounded && Input.GetKeyDown(KeyCode.Mouse0))
        {
            animator.SetTrigger(AnimatorConstants.ATTACK);
        }
    }

    public virtual void DamageTargets()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackPoint.position, stats.GetAttackRadius(), targetLayers);

        DameDealingInfo dameDealingInfo = CreateDamageDealingInfo();

        foreach (Collider2D target in colliders)
        {
            target.GetComponent<Entity>().TakeDamage(dameDealingInfo);
        }
    }

    public virtual void TakeDamage(DameDealingInfo dameDealingInfo)
    {
        stats.TakeDamage(dameDealingInfo);
    }

    protected virtual void Die()
    {
        animator.SetTrigger(AnimatorConstants.DEATH);
    }

    // Testing
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (stats != null && attackPoint != null)
            Gizmos.DrawWireSphere(attackPoint.position, stats.GetAttackRadius());

        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundCheckDistance);
    }
}
