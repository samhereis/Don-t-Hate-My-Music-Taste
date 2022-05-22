using Events;
using UnityEngine;

namespace Identifiers
{
    [CreateAssetMenu(fileName = "On Enemy Die", menuName = "Scriptables/Events/On Enemy Die")]
    public class OnEnemyDie : EventWithOneParameterBase<EnemyIdentifier>
    {

    }
}