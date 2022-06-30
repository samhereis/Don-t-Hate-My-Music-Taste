using Identifiers;
using UnityEngine;

namespace Samhereis.Events
{
    [CreateAssetMenu(fileName = "On Enemy Die", menuName = "Scriptables/Events/On Enemy Die")]
    public class OnEnemyDie : EventWithOneParameterBase<EnemyIdentifier>
    {

    }
}