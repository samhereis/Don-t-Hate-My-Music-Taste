using UnityEditor;
using UnityEngine;
using Samhereis.Exstensions;

namespace Samhereis.DI
{
    [CustomPropertyDrawer(typeof(BindDIScene.ObjectToDI))]
    public class ObjectToDIDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.indentLevel = 3;

            var rects = position.Row(new[] {0.2f, 1, 3f});

            EditorGUI.PropertyField(rects[0], property.FindPropertyRelative("IsUnbind"), new GUIContent());
            EditorGUI.PropertyField(rects[1], property.FindPropertyRelative("id"), new GUIContent());
            EditorGUI.PropertyField(rects[2], property.FindPropertyRelative("Instance"), new GUIContent());
        }
    }
}