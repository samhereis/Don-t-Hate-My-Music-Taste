using UnityEngine;
using UnityEngine.Events;

namespace Helpers.OnUnityEventDoers
{
    public class OnStartDo : MonoBehaviour
    {
        [SerializeField] private UnityEvent _onAwakeDo;
        [SerializeField][Range(0, 10)] private float _delay;

        private async void Start()
        {
            await AsyncHelper.DelayAndDo(_delay, () => _onAwakeDo?.Invoke());
        }
    }
}