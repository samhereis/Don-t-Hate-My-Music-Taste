using Samhereis.Events;
using UnityEngine;

namespace Events
{
    public class OnKeyWin : MonoBehaviour
    {
        [SerializeField] private EventWithNoParameters _eventWithNoParameters;
        [SerializeField] private KeyCode _key;

        private void Update()
        {
            //if (Input.GetKeyUp(_key)) _eventWithNoParameters?.Invoke();
        }
    }
}