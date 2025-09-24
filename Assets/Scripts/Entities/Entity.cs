using UnityEngine;

[RequireComponent(typeof(EntityStats), typeof(Rigidbody2D), typeof(Collider2D))]
public abstract class EntityBase : MonoBehaviour
{
    public Rigidbody2D Rb { get; private set; }
    public Animator Animator { get; private set; }
    public Collider2D EntityCollider { get; private set; }

    public EntityStateMachine StateMachine { get; protected set; }
    public FallDameSystem FallDameSystem { get; protected set; }
    public EntityStats Stats { get; private set; }


    [Header("Movement Details")]
    public float FacingDirection { get; private set; } = MovementConstants.FacingDirection.RIGHT;

    [Header("Collition Details")]
    [SerializeField] private float groundCheckDistance = 1.5f;
    [SerializeField] protected LayerMask groundLayer;
    public bool GroundDetected { get; private set; } = true;

    [Header("Attack Details")]
    public Transform AttackPoint;

    [Header("Collision Detection")]
    [SerializeField] protected Transform primaryWallCheck;
    [SerializeField] protected Transform secondaryWallCheck;
    [SerializeField] private Transform cliffCheckPoint;
    [SerializeField] protected float cliffCheckDistance = .5f;
    [SerializeField] protected float wallCheckDistance = 0.43f;
    public bool WallDetected { get; protected set; } = false;
    public bool CliffDetected { get; protected set; } = false;

    public virtual void Awake()
    {
        Stats = GetComponent<EntityStats>();
        Animator = GetComponentInChildren<Animator>();
        Rb = GetComponent<Rigidbody2D>();
        EntityCollider = GetComponent<Collider2D>();
    }

    public virtual void Start()
    {
        StateMachine = new EntityStateMachine();

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

        StateMachine.ActiveStateUpdate();
    }

    public float GetAttackDistance() => transform.DistanceXTo(AttackPoint.transform);

    public void MoveWithBaseSpeed(float direction)
    {
        SetVelocity(Stats.MoveSpeed * Mathf.Sign(direction), Rb.linearVelocityY);
    }

    public void MoveWithBaseSpeed()
    {
        SetVelocity(Stats.MoveSpeed * FacingDirection, Rb.linearVelocityY);
    }

    public void StopMovementX()
    {
        SetVelocity(0f, Rb.linearVelocityY);
    }

    public bool WithinAttackDistance(Transform target) => transform.DistanceXTo(target) <= GetAttackDistance();

    public void SetVelocity(float xVelocity, float yVelocity)
    {
        Rb.linearVelocity = new Vector2(xVelocity, yVelocity);
    }

    public void CurrentStateAnimationTrigger() => StateMachine.CurrentState.CurrentStateAnimationTrigger();

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

        WallDetected =
            Physics2D.Raycast(primaryWallCheck.position, Vector2.right * FacingDirection, wallCheckDistance, groundLayer) &&
            (secondaryWallCheck == null ||
                Physics2D.Raycast(secondaryWallCheck.position, Vector2.right * FacingDirection, wallCheckDistance, groundLayer));


        CliffDetected = !Physics2D.Raycast(cliffCheckPoint.position, Vector2.down, cliffCheckDistance, groundLayer);
    }

    // Testing
    protected virtual void OnDrawGizmos()
    {
#if UNITY_EDITOR
        if (Stats != null && AttackPoint != null)
        {
            Gizmos.color = Color.red;
            //Gizmos.DrawWireSphere(AttackPoint.WithLabel("Attack Range", Color.red).position, Stats.AttackRadius);
        }

        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.WithLabel("Ground Check", Color.green).position, transform.position + Vector3.down * groundCheckDistance);

        Gizmos.color = Color.red;
        if (primaryWallCheck != null)
        {
            Gizmos.DrawLine(primaryWallCheck.WithLabel("Wall Check 1", Color.red).position,
                primaryWallCheck.position + FacingDirection * wallCheckDistance * Vector3.right);
        }

        if (secondaryWallCheck != null)
        {
            Gizmos.DrawLine(secondaryWallCheck.WithLabel("Wall Check 2", Color.red).position,
                secondaryWallCheck.position + FacingDirection * wallCheckDistance * Vector3.right);
        }

        Gizmos.color = Color.blue;
        if (cliffCheckPoint != null)
        {
            Gizmos.DrawLine(cliffCheckPoint.WithLabel("Cliff Check", Color.blue).position,
                cliffCheckPoint.position + Vector3.down * cliffCheckDistance);
        }
#endif
    }
}
