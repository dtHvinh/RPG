using UnityEngine;

public static class TransformExtensions
{
    public static float DistanceTo(this Transform from, Transform to)
    {
        return Vector3.Distance(from.position, to.position);
    }

    public static float DistanceXTo(this Transform from, Transform to)
    {
        return Mathf.Abs(from.position.x - to.position.x);
    }
}
