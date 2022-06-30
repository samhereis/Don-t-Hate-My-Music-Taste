using UnityEngine.Events;
using UnityEngine;

namespace Samhereis.Helpers.OnUnityEventDoers
{
    public class OnUpdateDo : MonoBehaviour
    {
        [SerializeField] private UnityEvent _onUpdateDo;

        private void Update()
        {
            _onUpdateDo?.Invoke();
        }
    }
}