using UnityEngine;

public class NoFlip : MonoBehaviour
{
    private Quaternion originalRotation;

    private void Start()
    {
        originalRotation = transform.rotation;
    }

    private void LateUpdate()
    {
        transform.rotation = originalRotation;
    }
}
