using UnityEngine;

[RequireComponent(typeof(EntityStats), typeof(Rigidbody2D), typeof(Collider2D))]
public abstract class Entity : MonoBehaviour
{
    public Rigidbody2D Rb { get; private set; }
    public Animator Animator { get; private set; }
    public Collider2D Collider { get; private set; }

    public EntityStateMachine StateMachine { get; protected set; }

    public abstract float FacingDirection { get; }

    public virtual void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();
        Collider = GetComponent<Collider2D>();
        Animator = GetComponentInChildren<Animator>();
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

    public abstract void SetVelocity(float xVelocity, float yVelocity);

    protected virtual void Update()
    {
        StateMachine.ActiveStateUpdate();
    }

    public void CurrentStateAnimationTrigger() => StateMachine.CurrentState.CurrentStateAnimationTrigger();
}
