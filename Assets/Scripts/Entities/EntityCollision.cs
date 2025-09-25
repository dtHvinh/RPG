using UnityEngine;

public class EntityCollision : MonoBehaviour
{
    private Entity entity;

    [Header("Collition Details")]
    [SerializeField] private float groundCheckDistance = 1.5f;
    public LayerMask GroundLayer;
    public bool GroundDetected { get; private set; } = true;

    [Header("Collision Detection")]
    public Transform CliffCheckPoint;
    [SerializeField] protected Transform primaryWallCheck;
    [SerializeField] protected Transform secondaryWallCheck;
    [SerializeField] protected float cliffCheckDistance = .5f;
    [SerializeField] protected float wallCheckDistance = 0.43f;
    public bool WallDetected { get; protected set; } = false;
    public bool CliffDetected { get; protected set; } = false;

    private void Awake()
    {
        entity = GetComponentInParent<Entity>();
    }

    private void Update()
    {
        HandleCollisionDetection();
    }

    protected virtual void HandleCollisionDetection()
    {
        GroundDetected = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, GroundLayer);

        WallDetected =
            Physics2D.Raycast(primaryWallCheck.position, Vector2.right * entity.FacingDirection, wallCheckDistance, GroundLayer) &&
            (secondaryWallCheck == null ||
                Physics2D.Raycast(secondaryWallCheck.position, Vector2.right * entity.FacingDirection, wallCheckDistance, GroundLayer));


        CliffDetected = !Physics2D.Raycast(CliffCheckPoint.position, Vector2.down, cliffCheckDistance, GroundLayer);
    }
}
