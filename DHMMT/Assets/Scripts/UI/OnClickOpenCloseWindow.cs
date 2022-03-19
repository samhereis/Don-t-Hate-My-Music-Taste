using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
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

        [Header("Events")]
        [SerializeField] private OnClickOpenCloseWindowEvents _events;

        [Header("Settings")]
        [SerializeField] private bool _isSelfControlled = true;

        public void OnPointerClick(PointerEventData eventData)
        {
            if(_isSelfControlled) Do();
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

            _events._onOpen?.Invoke();
        }

        private async void Close()
        {
            foreach (var window in _closeThese)
            {
                window.Close();

                await Task.Yield();
            }

            _events._onClose?.Invoke();
        }
    }
}

[System.Serializable] internal class OnClickOpenCloseWindowEvents
{
    [SerializeField] internal UnityEvent _onOpen;
    [SerializeField] internal UnityEvent _onClose;
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
