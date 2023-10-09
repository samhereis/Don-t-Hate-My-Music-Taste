using ConstStrings;
using UnityEngine;
using UnityEngine.AI;

namespace Identifiers
{
    public class TheLight_Identifier : IdentifierBase
    {
        [Header(HeaderStrings.Components)]
        [SerializeField] private NavMeshAgent _navMeshAgent;

        [Header(HeaderStrings.Debug)]
        [SerializeField] private Exit_Identifier _exit;

        private void Start()
        {
            if (_navMeshAgent == null) _navMeshAgent = GetComponent<NavMeshAgent>();

            _exit = FindObjectOfType<Exit_Identifier>();

            _navMeshAgent.SetDestination(_exit.transform.position);
        }
    }
}