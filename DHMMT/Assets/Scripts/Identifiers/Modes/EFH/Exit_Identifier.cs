using ConstStrings;
using DI;
using Events;
using UnityEngine;

namespace Identifiers
{
    public class Exit_Identifier : IdentifierBase, IDIDependent
    {
        [SerializeField] private ExitLocation_Identifier[] _exitLocations;
        [DI(Event_DIStrings.onExitFound)][SerializeField] private EventWithOneParameters<Exit_Identifier> _onExitFound;

        private void Awake()
        {
            _exitLocations = FindObjectsOfType<ExitLocation_Identifier>();

            transform.position = _exitLocations[Random.Range(0, _exitLocations.Length)].transform.position;
        }

        private void Start()
        {
            (this as IDIDependent).LoadDependencies();
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