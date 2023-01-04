using UnityEngine;
using UnityEngine.Events;

namespace Helpers.OnUnityEventDoers
{
    public class OnAwakeDo : MonoBehaviour
    {
        [SerializeField] private UnityEvent _onAwakeDo;
        [SerializeField][Range(0, 10)] private float _delay;

        private async void Awake()
        {
            await AsyncHelper.DelayAndDo(_delay, () => _onAwakeDo?.Invoke());
        }
    }
}