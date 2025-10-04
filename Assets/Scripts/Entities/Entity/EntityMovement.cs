using UnityEngine;

[RequireComponent(typeof(IEntity))]
public class EntityMovement : MonoBehaviour, IMovement
{
    protected IEntity entity;

    [Range(0, 2)] public float MoveAnimSpeedMulti = 1f;

    [Header("Movement Details")]
    public float FacingDirection { get; private set; } = MovementConstants.FacingDirection.RIGHT;

    protected virtual void Awake()
    {
        entity = GetComponent<IEntity>();
    }

    protected virtual void Update()
    {
    }

    public void Stop()
    {
        SetVelocity(0f, entity.Rb.linearVelocityY);
    }

    public void Flip()
    {
        entity.Transform.Rotate(0f, 180f, 0f);
        FacingDirection *= -1;
    }

    public void SetVelocity(float xVelocity, float yVelocity)
    {
        entity.Rb.linearVelocity = new Vector2(xVelocity, yVelocity);
    }

    public void SetVelocity(Vector2 velocity)
    {
        entity.Rb.linearVelocity = velocity;
    }

    public void FacingToTarget(Transform target)
    {
        if (FacingDirection != DirectionToTarget(target))
            Flip();
    }
    public float DirectionToTarget(Transform target) => Mathf.Sign(target.position.x - entity.Transform.position.x);

}
