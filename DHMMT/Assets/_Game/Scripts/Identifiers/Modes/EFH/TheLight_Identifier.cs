using UnityEngine;
using UnityEngine.AI;

namespace Identifiers
{
    public class TheLight_Identifier : IdentifierBase
    {
        [Header("Components")]
        [SerializeField] private NavMeshAgent _navMeshAgent;

        [Header("Debug")]
        [SerializeField] private Exit_Identifier _exit;

        private void Start()
        {
            if (_navMeshAgent == null) _navMeshAgent = GetComponent<NavMeshAgent>();

            _exit = FindFirstObjectByType<Exit_Identifier>(FindObjectsInactive.Include);

            _navMeshAgent.SetDestination(_exit.transform.position);
        }
    }
}