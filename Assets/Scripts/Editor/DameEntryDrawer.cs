using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(SerializableDictionary<DamageType, float>))]
public class DamageDictionaryDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        var keys = property.FindPropertyRelative("keys");
        var values = property.FindPropertyRelative("values");

        property.isExpanded = EditorGUI.Foldout(position, property.isExpanded, label, true);

        if (property.isExpanded)
        {
            EditorGUI.indentLevel++;
            for (int i = 0; i < keys.arraySize; i++)
            {
                var keyProp = keys.GetArrayElementAtIndex(i);
                var valueProp = values.GetArrayElementAtIndex(i);

                Rect line = EditorGUILayout.GetControlRect();
                float half = line.width * 0.4f;

                Rect left = new(line.x, line.y, half - 5, line.height);
                Rect right = new(line.x + half, line.y, line.width - half, line.height);

                EditorGUI.PropertyField(left, keyProp, GUIContent.none);
                EditorGUI.PropertyField(right, valueProp, GUIContent.none);
            }

            if (GUILayout.Button("Add Entry"))
            {
                keys.arraySize++;
                values.arraySize++;
            }

            EditorGUI.indentLevel--;
        }

        EditorGUI.EndProperty();
    }
}
