using UnityEngine;

[RequireComponent(typeof(EntityBase))]
public class FallDameSystem : MonoBehaviour
{
    private EntityBase entity;

    [ReadOnly] public float fallTime;
    [SerializeField] private float dameFactor = 5f;
    [SerializeField] private float safeFallHeight = 3f;

    public FallDameSystem(EntityBase entity)
    {
        this.entity = entity;
    }

    private void Awake()
    {
        entity = GetComponent<EntityBase>();
    }

    private void Update()
    {
        if (entity.Rb.linearVelocityY < 0)
        {
            fallTime += Time.deltaTime;
        }
        else
        {
            fallTime = 0;
        }
    }
}
