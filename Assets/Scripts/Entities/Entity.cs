using UnityEngine;

//[RequireComponent(typeof(Rigidbody2D))]
//[RequireComponent(typeof(Collider2D))]
//[RequireComponent(typeof(FallDameSystem))]
//[RequireComponent(typeof(EntityStats))]
//public abstract class Entity<TCombat, TMovement, TCollision, THealth>
//    : MonoBehaviour, IEntity
//    where TCombat : EntityCombat
//    where TMovement : EntityMovement
//    where TCollision : EntityCollision
//    where THealth : EntityHealth
//{
//    public Rigidbody2D Rb { get; private set; }
//    public Animator Animator { get; private set; }
//    public Collider2D Collider { get; private set; }

//    public FallDameSystem FallDameSystem { get; protected set; }

//    public EntityStateMachine StateMachine { get; protected set; }
//    public EntityStats Stats { get; private set; }
//    public TCombat Combat { get; protected set; }
//    public TMovement Movement { get; protected set; }
//    public TCollision Collision { get; protected set; }
//    public THealth Health { get; protected set; }

//    public float FacingDirection => Movement.FacingDirection;

//    public virtual void Awake()
//    {
//        Stats = GetComponent<EntityStats>();
//        Rb = GetComponent<Rigidbody2D>();
//        Collider = GetComponent<Collider2D>();
//        FallDameSystem = GetComponent<FallDameSystem>();

//        Movement = GetComponentInChildren<TMovement>();
//        Animator = GetComponentInChildren<Animator>();
//        Combat = GetComponentInChildren<TCombat>();
//        Collision = GetComponentInChildren<TCollision>();
//        Health = GetComponentInChildren<THealth>();
//    }

//    public virtual void Start()
//    {
//        StateMachine = new EntityStateMachine();

//        InitializeStates();
//        SetInitialState();
//    }

//    /// <summary>
//    /// Initializes the set of states required for the implementing FSM correctly.   
//    /// </summary>
//    public abstract void InitializeStates();

//    /// <summary>
//    /// Sets the initial state of the FSM.
//    /// </summary>
//    public abstract void SetInitialState();

//    protected virtual void Update()
//    {
//        StateMachine.ActiveStateUpdate();
//    }

//    public void SetVelocity(float xVelocity, float yVelocity) => Movement.SetVelocity(xVelocity, yVelocity);

//    public void CurrentStateAnimationTrigger() => StateMachine.CurrentState.CurrentStateAnimationTrigger();
//}


//public abstract class Entity : Entity<EntityCombat, EntityMovement, EntityCollision, EntityHealth>
//{

//}
[RequireComponent(typeof(EntityStats), typeof(Rigidbody2D), typeof(Collider2D))]
[RequireComponent(typeof(FallDameSystem))]
public abstract class Entity : MonoBehaviour, IEntity<EntityCombat, EntityMovement, EntityCollision, EntityHealth>
{
    public Rigidbody2D Rb { get; private set; }
    public Animator Animator { get; private set; }
    public Collider2D Collider { get; private set; }
    public FallDameSystem FallDameSystem { get; protected set; }
    public EntityStateMachine StateMachine { get; protected set; }

    public EntityStats Stats { get; private set; }
    public EntityCombat Combat { get; protected set; }
    public EntityMovement Movement { get; protected set; }
    public EntityCollision Collision { get; protected set; }
    public EntityHealth Health { get; protected set; }

    public float FacingDirection => Movement.FacingDirection;

    public virtual void Awake()
    {
        Stats = GetComponent<EntityStats>();
        Rb = GetComponent<Rigidbody2D>();
        Collider = GetComponent<Collider2D>();
        FallDameSystem = GetComponent<FallDameSystem>();
        Movement = GetComponentInChildren<EntityMovement>();
        Animator = GetComponentInChildren<Animator>();
        Combat = GetComponentInChildren<EntityCombat>();
        Collision = GetComponentInChildren<EntityCollision>();
        Health = GetComponentInChildren<EntityHealth>();
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
        StateMachine.ActiveStateUpdate();
    }

    public void SetVelocity(float xVelocity, float yVelocity) => Movement.SetVelocity(xVelocity, yVelocity);

    public void CurrentStateAnimationTrigger() => StateMachine.CurrentState.CurrentStateAnimationTrigger();
}
