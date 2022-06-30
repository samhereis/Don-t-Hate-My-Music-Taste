using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using Samhereis.Helpers;
using Samhereis.UI;
using Samhereis.UI.Window;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Samhereis.UI
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
            if (_isSelfControlled) Do();
        }

        public void Do()
        {
            Open();
            Close();
        }

        private async void Open()
        {
            foreach (var window in _openThese) await AsyncHelper.Delay(() => window.Open());
            _events._onOpen?.Invoke();
        }

        private async void Close()
        {
            foreach (var window in _closeThese) await AsyncHelper.Delay(() => window.Close());
            _events._onClose?.Invoke();
        }
    }
}

[System.Serializable]
internal class OnClickOpenCloseWindowEvents
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
