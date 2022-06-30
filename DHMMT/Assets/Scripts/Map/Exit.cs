using Identifiers;
using Samhereis.Events;
using Samhereis.Helpers;
using UnityEngine;
using UnityEngine.AI;

public class Exit : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;

    [Header("Events")]
    [SerializeField] private EventWithNoParameters _onWin;

    private async void Awake()
    {
        _agent.enabled = false;
        await AsyncHelper.DelayAndDo(5, () => _agent.enabled = true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerIdentifier player)) _onWin?.Invoke();
    }

#if UNITY_EDITOR
    [ContextMenu("Setup")]
    public void Setup()
    {
        if (_agent == null) _agent = GetComponent<NavMeshAgent>();

        if (Application.isPlaying == false) _agent.enabled = false;
    }
#endif
}
