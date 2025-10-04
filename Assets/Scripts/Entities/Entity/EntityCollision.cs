using UnityEngine;

[RequireComponent(typeof(IMovement))]
public class EntityCollision : MonoBehaviour, ICollision
{
    private IMovement movement;

    [Header("Collition Details")]
    public bool GroundDetected { get; private set; } = true;
    public bool WallDetected { get; protected set; } = false;
    public bool CliffDetected { get; protected set; } = false;

    [Header("Collision Detection")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] protected Transform cliffCheckPoint;
    [SerializeField] protected Transform primaryWallCheck;
    [SerializeField] protected Transform secondaryWallCheck;
    [SerializeField] protected float cliffCheckDistance = .5f;
    [SerializeField] protected float wallCheckDistance = 0.43f;
    [SerializeField] private float groundCheckDistance = 1.5f;

    private void Awake()
    {
        movement = GetComponent<IMovement>();
    }

    private void Update()
    {
        HandleCollisionDetection();
    }

    protected virtual void HandleCollisionDetection()
    {
        GroundDetected =
            Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer);

        WallDetected =
            Physics2D.Raycast(primaryWallCheck.position, Vector2.right * movement.FacingDirection, wallCheckDistance, groundLayer) &&
            (secondaryWallCheck == null ||
                Physics2D.Raycast(secondaryWallCheck.position, Vector2.right * movement.FacingDirection, wallCheckDistance, groundLayer));

        CliffDetected =
            !Physics2D.Raycast(cliffCheckPoint.position, Vector2.down, cliffCheckDistance, groundLayer);
    }

    public Transform GetCliffCheckPoint() => cliffCheckPoint;

    public LayerMask GetGroundLayer() => groundLayer;

    private void OnDrawGizmos()
    {
        if (movement != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundCheckDistance);
            if (primaryWallCheck != null)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawLine(primaryWallCheck.WithLabel("Primary", Color.blue).position, primaryWallCheck.position + movement.FacingDirection * wallCheckDistance * Vector3.right);
            }
            if (secondaryWallCheck != null)
            {
                Gizmos.color = Color.cyan;
                Gizmos.DrawLine(secondaryWallCheck.WithLabel("Secondary", Color.blue).position, secondaryWallCheck.position + movement.FacingDirection * wallCheckDistance * Vector3.right);
            }
            if (cliffCheckPoint != null)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawLine(cliffCheckPoint.WithLabel("Cliff", Color.yellow).position, cliffCheckPoint.position + Vector3.down * cliffCheckDistance);
            }
        }
    }
}
