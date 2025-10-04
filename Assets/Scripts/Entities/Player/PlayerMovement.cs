using UnityEngine;

public class PlayerMovement : EntityMovement
{
    private Player player;

    [Header("Movement Details")]
    public float JumpAirResistance = 0.8f;
    public float DashSpeed = 20f;
    public float DashDuration = 0.2f;

    [Header("Slide Details")]
    public float SlideSpeed = 10f;
    public float SlideDuration = 0.5f;

    protected override void Awake()
    {
        base.Awake();

        player = GetComponentInParent<Player>();
    }

    protected override void Update()
    {
        base.Update();

        HandleFlip();
    }

    private void HandleFlip()
    {
        if (player.Combat.IsKnockbacking)
            return;

        if (player.Rb.linearVelocityX > 0 && FacingDirection == -1
            || player.Rb.linearVelocityX < 0 && FacingDirection == 1)
        {
            Flip();
        }
    }
}
