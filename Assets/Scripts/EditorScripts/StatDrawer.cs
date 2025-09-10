using UnityEditor;
using UnityEngine;
using static EntityStats;

[CustomPropertyDrawer(typeof(Stat))]
public class StatDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        SerializedProperty baseValueProp = property.FindPropertyRelative(nameof(Stat.baseValue));

        Rect labelRect = new(position.x, position.y, EditorGUIUtility.labelWidth, position.height);
        Rect valueRect = new(position.x + EditorGUIUtility.labelWidth, position.y,
                             position.width - EditorGUIUtility.labelWidth, position.height);

        EditorGUI.LabelField(labelRect, label);

        baseValueProp.floatValue = EditorGUI.FloatField(valueRect, baseValueProp.floatValue);

        EditorGUI.EndProperty();
    }
}
