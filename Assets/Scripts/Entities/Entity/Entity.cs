using UnityEngine;

[RequireComponent(typeof(EntityStats), typeof(Rigidbody2D), typeof(EntityStatusHandler))]
public abstract class Entity : MonoBehaviour, IEntity
{
    public Rigidbody2D Rb { get; private set; }
    public Animator Animator { get; private set; }
    public Transform Transform => transform;

    public EntityStateMachine StateMachine { get; protected set; }
    public EntityStatusHandler Status { get; protected set; }

    public abstract float FacingDirection { get; }

    public virtual void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();
        Status = GetComponent<EntityStatusHandler>();
        Animator = GetComponentInChildren<Animator>();
    }

    public virtual void Start()
    {
        StateMachine = new EntityStateMachine();
        InitializeStates();
        SetInitialState();
        SubscribeToEvents();
    }

    public virtual void Update()
    {
        if (StateMachine.CurrentState != null)
            StateMachine.ActiveStateUpdate();
    }

    public virtual void InitializeStates()
    {
    }

    public virtual void SetInitialState()
    {
    }

    public virtual void SubscribeToEvents()
    {
    }

    public abstract void Death();

    public abstract void SetVelocity(float xVelocity, float yVelocity);

    /// <summary>
    /// Try enter a state. If the entity is already in that state, do nothing.
    /// </summary>
    /// <param name="state">A state entity try to enter.</param>
    public bool TryEnterState(EntityState state)
    {
        if (StateMachine.CurrentState != state)
        {
            StateMachine.ChangeState(state);
            return true;
        }

        return false;
    }

    public void CurrentStateAnimationTrigger() => StateMachine.CurrentState.CurrentStateAnimationTrigger();
}
