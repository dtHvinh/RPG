using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class AreaBuffingObject : MonoBehaviour
{
    protected Vector3 startPos;

    protected virtual void Awake()
    {
        startPos = transform.position;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {

    }
}
