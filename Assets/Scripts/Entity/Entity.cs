using UnityEngine;

[RequireComponent(typeof(EntityStats), typeof(Rigidbody2D), typeof(Collider2D))]
public class Entity : MonoBehaviour
{
    protected EntityStateMachine stateMachine;

    public Rigidbody2D Rb { get; private set; }
    public Animator Animator { get; private set; }
    public Collider2D EntityCollider { get; private set; }

    [Header("Movement Details")]
    public float FacingDirection { get; private set; } = MovementConstants.FacingDirection.RIGHT;

    [Header("Collition Details")]
    [SerializeField] private float groundCheckDistance = 1.5f;
    [SerializeField] protected LayerMask groundLayer;
    public bool GroundDetected { get; private set; } = true;

    [Header("Attack Details")]
    [SerializeField] protected Transform attackPoint;
    [SerializeField] protected LayerMask targetLayers;

    public EntityStats Stats { get; private set; }

    public virtual void Awake()
    {
        Stats = GetComponent<EntityStats>();
        Animator = GetComponentInChildren<Animator>();
        Rb = GetComponent<Rigidbody2D>();
        EntityCollider = GetComponent<Collider2D>();
    }

    public virtual void Start()
    {

    }

    protected virtual void Update()
    {
        HandleCollisionDetection();
        HandleFlip();
    }

    public void SetVelocity(float xVelocity, float yVelocity)
    {
        Rb.linearVelocity = new Vector2(xVelocity, yVelocity);
    }

    public bool IsHorizontallyMoving() => Rb.linearVelocity.x != 0;

    private void HandleFlip()
    {
        if (Rb.linearVelocity.x > 0 && FacingDirection == -1 
            || Rb.linearVelocity.x < 0 && FacingDirection == 1)
        {
            Flip();
        }
    }

    public void Flip()
    {
        transform.Rotate(0f, 180f, 0f);
        FacingDirection *= -1;
    }

    // Collisions
    protected virtual void HandleCollisionDetection()
    {
        GroundDetected = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer);
    }

    // Testing
    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (Stats != null && attackPoint != null)
            Gizmos.DrawWireSphere(attackPoint.position, Stats.GetAttackRadius());

        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundCheckDistance);
    }
}
