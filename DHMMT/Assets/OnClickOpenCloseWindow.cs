using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Window
{
    [DisallowMultipleComponent]
    public class OnClickOpenCloseWindow : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private WindowBehaviorBase[] _openThese;
        [SerializeField] private WindowBehaviorBase[] _closeThese;

        private void OnValidate()
        {
            _button ??= GetComponent<Button>();
        }

        private void Awake()
        {
            _button.onClick.AddListener(Do);
        }

        private void Do()
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
