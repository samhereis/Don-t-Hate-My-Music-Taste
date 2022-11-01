using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Helpers
{
    public class GraphicHelper : MonoBehaviour
    {
        [SerializeField] private List<MeshRenderer> _meshRenderers;

        [ContextMenu(nameof(FindAllMeshRenderers))]
        public void FindAllMeshRenderers()
        {
            _meshRenderers = FindObjectsOfType<MeshRenderer>().ToList();
        }

        [ContextMenu(nameof(DisableAllShadows))]
        public async void DisableAllShadows()
        {
            foreach (MeshRenderer meshRenderer in _meshRenderers) await AsyncHelper.Delay(() => meshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off);
        }

        [ContextMenu(nameof(DisableAllRecieveShadows))]
        public async void DisableAllRecieveShadows()
        {
            foreach (MeshRenderer meshRenderer in _meshRenderers) await AsyncHelper.Delay(() => meshRenderer.receiveShadows = false);
        }
    }
}