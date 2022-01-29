using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helpers;
using UnityEngine.AI;

public class Exit : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;

    private async void Awake()
    {
        _agent.enabled = false;

        await AsyncHelper.Delay(5);

        _agent.enabled = true;
    }

#if UNITY_EDITOR
    [ContextMenu("Setup")]
    public void Setup()
    {
        if (!_agent) _agent = GetComponent<NavMeshAgent>();

        if (Application.isPlaying == false)
        {
            _agent.enabled = false;
        }
    }
#endif
}
