using UnityEngine.Events;
using UnityEngine;

namespace Samhereis.Helpers.OnUnityEventDoers
{
    public class OnDisableDo : MonoBehaviour
    {
        [SerializeField] private UnityEvent _onDisable = new UnityEvent();

        private void OnDisable()
        {
            _onDisable?.Invoke();
        }
    }
}