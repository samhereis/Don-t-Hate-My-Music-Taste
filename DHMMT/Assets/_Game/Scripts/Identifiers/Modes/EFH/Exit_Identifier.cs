using ConstStrings;
using DependencyInjection;
using Observables;
using UnityEngine;

namespace Identifiers
{
    public class Exit_Identifier : IdentifierBase, INeedDependencyInjection
    {
        [SerializeField] private ExitLocation_Identifier[] _exitLocations;

        [Inject(DataSignal_ConstStrings.onExitFound)] private DataSignal<Exit_Identifier> _onExitFound;

        private void Awake()
        {
            _exitLocations = FindObjectsByType<ExitLocation_Identifier>(FindObjectsInactive.Include, FindObjectsSortMode.None);

            transform.position = _exitLocations[Random.Range(0, _exitLocations.Length)].transform.position;
        }

        private void Start()
        {
            DependencyContext.diBox.InjectDataTo(this);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<PlayerIdentifier>(out var playerIdentifier))
            {
                _onExitFound?.Invoke(this);
            }
        }
    }
}