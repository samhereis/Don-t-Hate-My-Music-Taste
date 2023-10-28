using ConstStrings;
using Helpers;
using System.Collections.Generic;
using System.Linq;
using Unity.AI.Navigation;
using UnityEngine;

namespace Identifiers
{
    public class TerrainReactable_Identifier : MonoBehaviour
    {
        [Header(HeaderStrings.Settings)]
        [SerializeField] private float _xSize;
        [SerializeField] private float _upYSize;
        [SerializeField] private float _downYSize;
        [SerializeField] private float _zSize;
        [SerializeField] private float _xOffset;
        [SerializeField] private float _zOffset;

        [SerializeField] private bool _autoUpdate = false;

        [Header(HeaderStrings.Components)]
        [SerializeField] private NavMeshLink _topNavMeshLink;
        //[SerializeField] private NavMeshLink _bottomNavMeshLink;
        [SerializeField] private NavMeshLink _rightNavMeshLink;
        //[SerializeField] private NavMeshLink _leftNavMeshLink;

        [SerializeField] private Transform[] _grassList;
        [SerializeField] private float _xGrassDistance;
        [SerializeField] private float _firstGrassXPosition;

        [SerializeField] private Transform[] _grassRowList;
        [SerializeField] private float _zGrassRowDistance;
        [SerializeField] private float _firstGrassRowZPosition;

        [ContextMenu(nameof(Setup))]
        public void Setup()
        {
            foreach (var navMeshLink in GetComponents<NavMeshLink>())
            {
                DestroyImmediate(navMeshLink);
            }

            if (_topNavMeshLink == null) { _topNavMeshLink = gameObject.AddComponent<NavMeshLink>(); }
            //if (_bottomNavMeshLink == null) { _bottomNavMeshLink = gameObject.AddComponent<NavMeshLink>(); }
            if (_rightNavMeshLink == null) { _rightNavMeshLink = gameObject.AddComponent<NavMeshLink>(); }
            //if (_leftNavMeshLink == null) { _leftNavMeshLink = gameObject.AddComponent<NavMeshLink>(); }

            _topNavMeshLink.width = _xSize;
            //_bottomNavMeshLink.width = _xSize;
            _rightNavMeshLink.width = _zSize;
            //_leftNavMeshLink.width = _zSize;

            _topNavMeshLink.autoUpdate = _autoUpdate;
            //_bottomNavMeshLink.autoUpdate = _autoUpdate;
            _rightNavMeshLink.autoUpdate = _autoUpdate;
            //_leftNavMeshLink.autoUpdate = _autoUpdate;

            _topNavMeshLink.startPoint = new Vector3(0, _upYSize, (_zSize / 2) - _zOffset);
            _topNavMeshLink.endPoint = new Vector3(0, _downYSize, (_zSize / 2) + _zOffset * 2);

            //_bottomNavMeshLink.startPoint = new Vector3(0, _upYSize, -((_zSize / 2) - _zOffset));
            //_bottomNavMeshLink.endPoint = new Vector3(0, _downYSize, -((_zSize / 2) + _zOffset * 2));

            _rightNavMeshLink.startPoint = new Vector3((_xSize / 2) - _xOffset, _upYSize, 0);
            _rightNavMeshLink.endPoint = new Vector3((_xSize / 2) + _xOffset * 2, _downYSize, 0);

            //_leftNavMeshLink.startPoint = new Vector3(-((_xSize / 2) - _xOffset), _upYSize, 0);
            //_leftNavMeshLink.endPoint = new Vector3(-((_xSize / 2) + _xOffset * 2), _downYSize, 0);
        }

        [ContextMenu(nameof(SetupGrass))]
        public void SetupGrass()
        {
            _grassRowList = GetComponentsInChildren<Transform>(true).Where(x =>
            {
                var isGrass = x.gameObject.name.StartsWith("GrassRow") && x.GetComponent<MeshRenderer>() == null;
                return isGrass;
            }).ToArray();

            var z = _firstGrassRowZPosition - _zGrassRowDistance;

            var rotationList = new List<float>() { 0f, 90, 180, 270 };

            foreach (var grassRow in _grassRowList)
            {
                _grassList = grassRow.GetComponentsInChildren<Transform>(true).Where(x =>
                {
                    var isGrass = x.gameObject.name.StartsWith("Heather") && x.GetComponent<MeshRenderer>() != null; ;
                    return isGrass;
                }).ToArray();

                var x = _firstGrassXPosition + _xGrassDistance;

                foreach (var grass in _grassList)
                {
                    x -= _xGrassDistance;
                    grass.transform.localPosition = new Vector3(x, 0, Random.Range(-0.025f, 0.025f));
                    grass.transform.localEulerAngles = new Vector3(0, rotationList.GetRandom(), 0);
                }


                z += _zGrassRowDistance;
                grassRow.transform.localPosition = new Vector3(0, 0.5f, z);
            }
        }
    }
}