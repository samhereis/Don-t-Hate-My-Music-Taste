using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
#if UNITY_EDITOR
using UnityEditor;
using UI.Window;
#endif

namespace UI.Window
{
    [DisallowMultipleComponent]
    public class OnClickOpenCloseWindow : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private WindowBehaviorBase[] _openThese;
        [SerializeField] private WindowBehaviorBase[] _closeThese;

        public void OnPointerClick(PointerEventData eventData)
        {
            Do();
        }

        public void Do()
        {
            Open();
            Close();
        }

        private async void Open()
        {
            foreach (var window in _openThese)
            {
                window.Open();

                await Task.Yield();
            }
        }

        private async void Close()
        {
            foreach (var window in _closeThese)
            {
                window.Close();

                await Task.Yield();
            }
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(OnClickOpenCloseWindow))]
public class OnClickOpenCloseWindowEditor : UnityEditor.Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
    }
}
#endif
