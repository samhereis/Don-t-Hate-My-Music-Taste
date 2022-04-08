using Helpers;
using UnityEngine;

namespace Sripts
{
    public sealed class TerrainModifier : MonoBehaviour
    {
        [Header("Componenets")]
        [SerializeField] private Terrain _terrain;

        private float[,] _heights;
        private float[,] _InitialHeights;

        private void Awake()
        {
            _InitialHeights = _terrain.terrainData.GetHeights(0, 0, _terrain.terrainData.heightmapResolution, _terrain.terrainData.heightmapResolution);
            _heights = _terrain.terrainData.GetHeights(0, 0, _terrain.terrainData.heightmapResolution, _terrain.terrainData.heightmapResolution);
        }

        private void OnDisable()
        {
            _terrain.terrainData.SetHeights(0, 0, _InitialHeights);
        }

        public async void Deform(Vector3 point)
        {
            await AsyncHelper.Delay();

            point = point - transform.position;

            point.x = (point.x / _terrain.terrainData.size.x);
            point.y = (2 / _terrain.terrainData.size.y);
            point.z = (point.z / _terrain.terrainData.size.z);

            int mouseX = (int)(point.z * _terrain.terrainData.heightmapResolution);
            int mouseZ = (int)(point.x * _terrain.terrainData.heightmapResolution);

            try
            {
                _heights[mouseX, mouseZ] = point.y; 
                _terrain.terrainData.SetHeights(0, 0, _heights);
            }  catch { Debug.LogWarning("�rrot: " + _heights.Length + "  " + mouseX + "  " + mouseZ + " " + _terrain.terrainData.heightmapResolution); }
        }

#if UNITY_EDITOR
        [ContextMenu("Setup")] public void Setup()
        {
            if (_terrain == null) _terrain = GetComponent<Terrain>();
        }
#endif
    }
}