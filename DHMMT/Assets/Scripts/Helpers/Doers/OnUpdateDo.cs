using UnityEngine.Events;
using UnityEngine;

public class OnUpdateDo : MonoBehaviour
{
    [SerializeField] private UnityEvent _onUpdateDo;

    private void Update()
    {
        _onUpdateDo?.Invoke();
    }
}
