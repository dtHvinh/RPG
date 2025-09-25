using UnityEngine;

[RequireComponent(typeof(Entity))]
public class FallDameSystem : MonoBehaviour
{
    private Entity entity;

    [SerializeField] private float safeFallHeight = 3f;

    public FallDameSystem(Entity entity)
    {
        this.entity = entity;
    }

    private void Awake()
    {
        entity = GetComponent<Entity>();
    }

    public bool CheckSafeFall(Transform checkPoint)
    {
        RaycastHit2D hit = Physics2D.Raycast(checkPoint.position, Vector2.down, safeFallHeight, entity.Collision.GroundLayer);
        return hit.collider != null;
    }
}
