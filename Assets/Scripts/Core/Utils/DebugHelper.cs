using UnityEditor;
using UnityEngine;

public static class DebugHelper
{
    public static bool IsDebugEnabled = true;

    public static GUIStyle GetSmallGUIStyle(Color color)
    {
        GUIStyle smallLabel = new GUIStyle();
        smallLabel.fontSize = 10;
        smallLabel.normal.textColor = color;

        return smallLabel;
    }

#if UNITY_EDITOR
    /// <summary>
    /// Display a small text label above the transform's position in the Scene view. Debug use only.
    /// </summary>
    public static Transform WithLabel(this Transform transform, string text, Color color)
    {
        Handles.Label(transform.position + Vector3.up * 0.2f, text, GetSmallGUIStyle(color));
        return transform;
    }

    /// <summary>
    /// Display a small text label above the transform's position in the Scene view. Debug use only.
    /// </summary>
    public static Vector3 WithLabel(this Vector3 vector, string text, Color color)
    {
        Handles.Label(vector + Vector3.up * 0.2f, text, GetSmallGUIStyle(color));
        return vector;
    }
#endif
}
