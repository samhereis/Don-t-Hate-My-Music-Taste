using UnityEngine;
using UnityEngine.Events;

namespace Scriptables.Values
{
    [CreateAssetMenu(fileName = "New Bool Value", menuName = "Scriptables/Values/Bool")]
    public class BoolValue_SO : Value_SO_Base<bool>
    {
        public override bool value => _value;

        protected override UnityEvent<bool> onValueChange { get; } = new UnityEvent<bool>();
    }
}