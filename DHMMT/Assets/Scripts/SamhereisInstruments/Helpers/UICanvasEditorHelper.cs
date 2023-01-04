#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Helpers
{
    [DisallowMultipleComponent]
    public class UICanvasEditorHelper : MonoBehaviour
    {
        [SerializeField] private bool _rename = false;

        [ContextMenu("Auto Configure")]
        public void Hide()
        {
            gameObject.SetActive(false);
            transform.localScale = Vector3.zero;
        }

        private void OnValidate()
        {
            string objName = gameObject.name;
            objName = objName.Replace("Window", "Canvas");
            gameObject.name = objName;

            if (gameObject.name.StartsWith("Canvas") == false & _rename) gameObject.name = "Window    " + gameObject.name + "    Window";
        }

        [ContextMenu("Change")]
        public void Show()
        {
            foreach (var window in transform.parent.GetComponentsInChildren<UIWindowEditorHelper>()) window.Hide();

            gameObject.SetActive(true);
            transform.localScale = Vector3.one;
        }
    }

    [CustomEditor(typeof(UICanvasEditorHelper))]
    public class UICanvasEditorHelperEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            UICanvasEditorHelper editorHelper = (UICanvasEditorHelper)target;
            GUILayout.BeginHorizontal();

            if (GUILayout.Button(nameof(editorHelper.Hide))) editorHelper.Hide();
            if (GUILayout.Button(nameof(editorHelper.Show))) editorHelper.Show();

            GUILayout.EndHorizontal();
        }
    }
}
#endif