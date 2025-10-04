using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(StatusEffect), true)]
public class StatusEffectDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        string typeName = property.managedReferenceFullTypename;
        if (!string.IsNullOrEmpty(typeName))
        {
            var split = typeName.Split(' ');
            label = new GUIContent(split[^1]);
        }

        EditorGUI.PropertyField(position, property, label, true);

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property, label, true);
    }
}
