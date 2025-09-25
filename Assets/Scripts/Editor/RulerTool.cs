using UnityEditor;
using UnityEngine;

public class RulerTool : EditorWindow
{
    private Transform pointA;
    private Transform pointB;

    [MenuItem("Tools/Ruler Tool")]
    private static void OpenWindow()
    {
        GetWindow<RulerTool>("Ruler Tool");
    }

    private void OnGUI()
    {
        GUILayout.Label("Select two points to measure distance", EditorStyles.boldLabel);

        pointA = (Transform)EditorGUILayout.ObjectField("Point A", pointA, typeof(Transform), true);
        pointB = (Transform)EditorGUILayout.ObjectField("Point B", pointB, typeof(Transform), true);

        if (pointA != null && pointB != null)
        {
            float distance = Vector3.Distance(pointA.position, pointB.position);
            EditorGUILayout.LabelField("Distance:", distance.ToString("F2"));
        }
    }
}
