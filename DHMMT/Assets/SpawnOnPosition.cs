using Helpers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOnPosition : MonoBehaviour
{
    [Header("What to spawn")]
    [SerializeField] private GameObject _object;

    [Header("Terrain")]
    [SerializeField] private TerrainData _terrain;

    /*private async void Awake()
    {
        float width = (float)_terrain.heightmapWidth;
        float height = (float)_terrain.heightmapHeight;

        foreach (TreeInstance tree in _terrain.treeInstances)
        {
            Vector3 position = new Vector3(tree.position.x * width, tree.position.y, tree.position.z * height);

            Instantiate(_object, position + Vector3.one * 5, transform.rotation, transform);

            await AsyncHelper.Delay();
        }
    }*/
}
