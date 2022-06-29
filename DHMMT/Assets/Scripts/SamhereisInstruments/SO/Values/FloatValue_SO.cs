using UnityEngine;
using UnityEngine.Events;

namespace Scriptables.Values
{
    [CreateAssetMenu(fileName = "New Float Value", menuName = "Scriptables/Values/Float")]
    public class FloatValue_SO : Value_SO_Base<float>
    {
        public override float value => _value;

        protected override UnityEvent<float> onValueChange { get; } = new UnityEvent<float>();
    }
}