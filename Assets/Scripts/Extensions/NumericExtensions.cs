using UnityEngine;

public static class NumericExtensions 
{
    public static float Half(this float value) => value / 2f;

    public static int Half(this int value) => value / 2;

    /// <summary>
    /// Calculates the absolute distance between the specified floating-point value and another value.  
    /// </summary>
    /// <param name="value">The first floating-point value to compare.</param>
    /// <param name="other">The second floating-point value to compare against.</param>
    /// <returns>The absolute difference between <paramref name="value"/> and <paramref name="other"/>.</returns>
    public static float DistaceTo(this float value, float other) => Mathf.Abs(value - other);
}
