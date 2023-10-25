using UnityEngine;
using UnityEngine.Events;

namespace GameplayEvents
{
    public class OnBecomeVisibleInvisible : MonoBehaviour
    {
        public UnityEvent onBecomeVisible;
        public UnityEvent onBecomeInvisible;

        [SerializeField] private MonoBehaviour[] _activateOnVisible;
        [SerializeField] private MonoBehaviour[] _deactivateOnVisible;

        private void OnBecameVisible()
        {
            SetVisible(true);

            onBecomeVisible?.Invoke();
        }

        private void OnBecameInvisible()
        {
            SetVisible(false);

            onBecomeInvisible?.Invoke();
        }

        private void SetVisible(bool visible)
        {
            foreach (var monobeh in _activateOnVisible)
            {
                monobeh.enabled = visible == true;
            }

            foreach (var monobeh in _deactivateOnVisible)
            {
                monobeh.enabled = visible == false;
            }
        }
    }
}