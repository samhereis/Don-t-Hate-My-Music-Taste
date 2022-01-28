using UnityEngine;
using UnityEngine.Events;

public class OnEnableDo : MonoBehaviour
{
    [SerializeField] private UnityEvent _onEnableDo;

    private void OnEnable()
    {
        _onEnableDo?.Invoke();
    }
}
