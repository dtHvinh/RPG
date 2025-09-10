using UnityEngine;

public class Player : Entity
{
    private float xInput;

    protected override void Update()
    {
        HandleInput();
        base.Update();
    }

    protected override void HandleMovement()
    {
        if (canMove)
            rb.linearVelocity = new Vector2(xInput * moveSpeed, rb.linearVelocity.y);
        else
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);


        facingDirection = xInput == 0 ? facingDirection : Mathf.Sign(xInput);
    }

    protected virtual void HandleInput()
    {
        xInput = Input.GetAxis("Horizontal");

        if (isGrounded && Input.GetKey(KeyCode.Space))
            TryToJump();
    }

}
