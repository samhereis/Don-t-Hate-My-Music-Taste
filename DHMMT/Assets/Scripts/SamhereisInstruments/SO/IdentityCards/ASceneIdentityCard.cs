using UnityEngine;

namespace IdentityCards
{
    [CreateAssetMenu(fileName = "Scene ", menuName = "Scriptables/IdentityCards/Scene")]
    public sealed class ASceneIdentityCard : IdentityCardBase<string>
    {
        [field: SerializeField] public Sprite icon { get; private set; }
    }
}