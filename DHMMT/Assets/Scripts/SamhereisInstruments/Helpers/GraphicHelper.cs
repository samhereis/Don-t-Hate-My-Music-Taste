using Helpers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
        await AsyncHelper.Delay(() =>
        {
            foreach(MeshRenderer meshRenderer in _meshRenderers) meshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        });
    }

    [ContextMenu(nameof(DisableAllRecieveShadows))]
    public async void DisableAllRecieveShadows()
    {
        await AsyncHelper.Delay(() =>
        {
            foreach (MeshRenderer meshRenderer in _meshRenderers) meshRenderer.receiveShadows = false;
        });
    }
}
