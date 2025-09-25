using UnityEngine;

public class EntityMovement : MonoBehaviour
{
    protected Entity entity;

    [Header("Movement Details")]
    public float FacingDirection { get; private set; } = MovementConstants.FacingDirection.RIGHT;

    private void Awake()
    {
        entity = GetComponentInParent<Entity>();
    }

    private void Update()
    {
        HandleFlip();
    }

    private void HandleFlip()
    {
        if (entity.Rb.linearVelocity.x > 0 && FacingDirection == -1
            || entity.Rb.linearVelocity.x < 0 && FacingDirection == 1)
        {
            Flip();
        }
    }

    public void StopMovementX()
    {
        SetVelocity(0f, entity.Rb.linearVelocityY);
    }

    public void Flip()
    {
        entity.transform.Rotate(0f, 180f, 0f);
        FacingDirection *= -1;
    }

    public void SetVelocity(float xVelocity, float yVelocity)
    {
        entity.Rb.linearVelocity = new Vector2(xVelocity, yVelocity);
    }

    public void FacingToTarget(Transform target)
    {
        if (entity.FacingDirection != DirectionToTarget(target))
            Flip();
    }
    public float DirectionToTarget(Transform target) => Mathf.Sign(target.position.x - entity.transform.position.x);

}
