using UnityEngine;
using UnityEngine.Events;

public class OnAwakeDo : MonoBehaviour
{
    [SerializeField] private UnityEvent _onAwakeDo;

    private void Awake()
    {
        _onAwakeDo?.Invoke();
    }
}
