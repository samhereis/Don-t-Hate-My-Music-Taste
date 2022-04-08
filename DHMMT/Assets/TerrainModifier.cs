using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.Events;
using Helpers;

namespace Sripts
{
    public sealed class TerrainModifier : MonoBehaviour
    {
        [Header("Componenets")]
        [SerializeField] private Terrain _terrain;

        private int heithMapWidth;
        private int heithMapHeight;
        private float[,] heights;
        private float[,] InitialHeights;

        private void OnValidate()
        {
            if(_terrain == null) _terrain = GetComponent<Terrain>();
        }

        private void Awake()
        {
            InitialHeights = _terrain.terrainData.GetHeights(0, 0, _terrain.terrainData.heightmapResolution, _terrain.terrainData.heightmapResolution);
            heights = _terrain.terrainData.GetHeights(0, 0, _terrain.terrainData.heightmapResolution, _terrain.terrainData.heightmapResolution);
        }

        private void Update()
        {
            //if(Input.GetMouseButton(0)) if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit raycast)) Deform(raycast.point);
        }

        private void OnDisable()
        {
            _terrain.terrainData.SetHeights(0, 0, InitialHeights);
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

            //mouseX = Mathf.Clamp(mouseX, 0, _terrain.terrainData.heightmapResolution - 1);
            //mouseZ = Mathf.Clamp(mouseZ, 0, _terrain.terrainData.heightmapResolution - 1);

            try
            {
                heights[mouseX, mouseZ] = point.y;
            }
            catch
            {
                Debug.LogWarning(heights.Length +"  " +  mouseX + "  " + mouseZ + " " + _terrain.terrainData.heightmapResolution);
            }

            _terrain.terrainData.SetHeights(0, 0, heights);
        }

#if UNITY_EDITOR
        [ContextMenu("Setup")] public void Setup()
        {

        }
#endif
    }
}