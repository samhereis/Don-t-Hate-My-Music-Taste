using Helpers;
using UnityEngine;
using UnityEngine.Events;

public class OnEnableDo : MonoBehaviour
{
    [SerializeField] private UnityEvent _onEnableDo;
    [SerializeField][Range(0, 10)] private float _delay;

    private async void OnEnable()
    {
        await AsyncHelper.Delay(_delay);

        _onEnableDo?.Invoke();
    }
}
