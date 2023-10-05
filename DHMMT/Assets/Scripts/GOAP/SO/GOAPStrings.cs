using Helpers;
using UnityEngine;

namespace SO.GOAP
{
    [CreateAssetMenu(fileName = "GOAPPreCondition", menuName = "ScriptableObjects/GOAP/GOAPPreCondition")]
    public class GOAPStrings : ScriptableObject
    {
        [field: SerializeField] public string preCondition { get; private set; } = "PreCondition";

        private void OnValidate()
        {
            if(preCondition != name)
            {
                preCondition = name;
                this.TrySetDirty();
            }
        }
    }
}