using IdentityCards;
using System.Collections.Generic;
using UnityEngine;

namespace Holders
{
    [CreateAssetMenu(fileName = "AllScenesHolder ", menuName = "Scriptables/Holder/AllScenesHolder")]
    public sealed class AllScenesHolder : ScriptableObject
    {
        [SerializeField] private List<ASceneIdentityCard> _allScenes = new List<ASceneIdentityCard>();
        public IEnumerable<ASceneIdentityCard> allScenes => _allScenes;
    }
}