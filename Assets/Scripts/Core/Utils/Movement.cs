using UnityEngine;

public class Movement
{
    public static Vector2 ToJumpForce(float jumpForce, float angleInDegrees)
    {
        float angleInRadians = angleInDegrees * Mathf.Deg2Rad;
        float x = jumpForce * Mathf.Cos(angleInRadians);
        float y = jumpForce * Mathf.Sin(angleInRadians);
        return new Vector2(x, y);
    }
}
