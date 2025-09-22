using UnityEngine;

[RequireComponent(typeof(EntityStats), typeof(Rigidbody2D), typeof(Collider2D))]
public abstract class Entity : MonoBehaviour
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

    [Header("Collision Detection")]
    [SerializeField] protected Transform primaryWallCheck;
    [SerializeField] protected Transform secondaryWallCheck;
    [SerializeField] private Transform cliffCheckPoint;
    [SerializeField] protected float cliffCheckDistance = .5f;
    [SerializeField] protected float wallCheckDistance = 0.43f;
    public bool WallDetected { get; protected set; } = false;
    public bool CliffDetected { get; protected set; } = false;

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
        stateMachine = new EntityStateMachine();

        InitializeStates();
        SetInitialState();
    }

    /// <summary>
    /// Initializes the set of states required for the implementing FSM correctly.   
    /// </summary>
    public abstract void InitializeStates();

    /// <summary>
    /// Sets the initial state of the FSM.
    /// </summary>
    public abstract void SetInitialState();

    protected virtual void Update()
    {
        HandleCollisionDetection();
        HandleFlip();

        stateMachine.ActiveStateUpdate();
    }

    public void MoveWithBaseSpeed(float direction)
    {
        SetVelocity(Stats.GetMoveSpeed() * Mathf.Sign(direction), Rb.linearVelocityY);
    }

    public void MoveWithBaseSpeed()
    {
        SetVelocity(Stats.GetMoveSpeed() * FacingDirection, Rb.linearVelocityY);
    }

    public bool IsHorizontallyMoving() => Rb.linearVelocity.x != 0;

    public void SetVelocity(float xVelocity, float yVelocity)
    {
        Rb.linearVelocity = new Vector2(xVelocity, yVelocity);
    }

    public virtual bool CanMove() => !WallDetected && !CliffDetected;

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

        WallDetected = Physics2D.Raycast(primaryWallCheck.position, Vector2.right * FacingDirection, wallCheckDistance, groundLayer)
            && secondaryWallCheck != null ?
                Physics2D.Raycast(secondaryWallCheck.position, Vector2.right * FacingDirection, wallCheckDistance, groundLayer)
                : true;

        CliffDetected = !Physics2D.Raycast(cliffCheckPoint.position, Vector2.down, cliffCheckDistance, groundLayer);
    }

    // Testing
    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (Stats != null && attackPoint != null)
            Gizmos.DrawWireSphere(attackPoint.position, Stats.GetAttackRadius());

        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundCheckDistance);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(primaryWallCheck.position, primaryWallCheck.position + FacingDirection * wallCheckDistance * Vector3.right);
        if (secondaryWallCheck != null)
            Gizmos.DrawLine(secondaryWallCheck.position, secondaryWallCheck.position + FacingDirection * wallCheckDistance * Vector3.right);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(cliffCheckPoint.position, cliffCheckPoint.position + Vector3.down * cliffCheckDistance);
    }
}
