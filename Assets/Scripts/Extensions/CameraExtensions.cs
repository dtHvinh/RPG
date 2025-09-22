using UnityEngine;

public static class CameraExtensions
{
    public static float GetWidth(this Camera camera) => camera.orthographicSize * 2 * camera.aspect;

    public static Vector3 GetTransformPosition(this Camera camera) => camera.transform.position;
}
