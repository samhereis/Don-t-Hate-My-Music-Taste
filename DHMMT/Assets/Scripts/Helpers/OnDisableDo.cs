using UnityEngine.Events;
using UnityEngine;

public class OnDisableDo : MonoBehaviour
{
    [SerializeField] private UnityEvent onDisable = new UnityEvent();

    private void OnDisable() 
    {
        onDisable?.Invoke();
    }
}
