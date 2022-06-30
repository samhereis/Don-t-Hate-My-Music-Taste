using UnityEngine;
using UnityEngine.Events;

namespace Samhereis.Values
{
    [CreateAssetMenu(fileName = "New Int Value", menuName = "Scriptables/Values/Int")]
    public class IntValue_SO : Value_SO_Base<int>
    {
        public override int value => _value;
        protected override UnityEvent<int> onValueChange { get; } = new UnityEvent<int>();
    }
}