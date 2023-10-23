using UnityEngine;
using UnityEngine.Events;

namespace GameplayEvents
{
    public class OnTriggerEnterExit : MonoBehaviour
    {
        [SerializeField] private UnityEvent<Collider> _ontriggerEnter = new UnityEvent<Collider>();
        [SerializeField] private UnityEvent<Collider> _ontriggerExit = new UnityEvent<Collider>();

        private void OnTriggerEnter(Collider other)
        {
            _ontriggerEnter?.Invoke(other);
        }

        private void OnTriggerExit(Collider other)
        {
            _ontriggerExit?.Invoke(other);
        }
    }
}