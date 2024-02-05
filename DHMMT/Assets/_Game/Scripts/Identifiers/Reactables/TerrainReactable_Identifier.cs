using Unity.AI.Navigation;
using UnityEngine;

namespace Identifiers
{
    public class TerrainReactable_Identifier : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float _xSize;
        [SerializeField] private float _upYSize;
        [SerializeField] private float _downYSize;
        [SerializeField] private float _zSize;
        [SerializeField] private float _xOffset;
        [SerializeField] private float _zOffset;

        [SerializeField] private bool _autoUpdate = false;

        [Header("Components")]
        [SerializeField] private NavMeshLink _topNavMeshLink;
        [SerializeField] private NavMeshLink _bottomNavMeshLink;
        [SerializeField] private NavMeshLink _rightNavMeshLink;
        [SerializeField] private NavMeshLink _leftNavMeshLink;

        [ContextMenu(nameof(Setup))]
        public void Setup()
        {
            foreach (var navMeshLink in GetComponents<NavMeshLink>())
            {
                DestroyImmediate(navMeshLink);
            }

            if (_topNavMeshLink == null) { _topNavMeshLink = gameObject.AddComponent<NavMeshLink>(); }
            if (_bottomNavMeshLink == null) { _bottomNavMeshLink = gameObject.AddComponent<NavMeshLink>(); }
            if (_rightNavMeshLink == null) { _rightNavMeshLink = gameObject.AddComponent<NavMeshLink>(); }
            if (_leftNavMeshLink == null) { _leftNavMeshLink = gameObject.AddComponent<NavMeshLink>(); }

            _topNavMeshLink.width = _xSize;
            _bottomNavMeshLink.width = _xSize;
            _rightNavMeshLink.width = _zSize;
            _leftNavMeshLink.width = _zSize;

            _topNavMeshLink.autoUpdate = _autoUpdate;
            _bottomNavMeshLink.autoUpdate = _autoUpdate;
            _rightNavMeshLink.autoUpdate = _autoUpdate;
            _leftNavMeshLink.autoUpdate = _autoUpdate;

            _topNavMeshLink.startPoint = new Vector3(0, _upYSize, (_zSize / 2) - _zOffset);
            _topNavMeshLink.endPoint = new Vector3(0, _downYSize, (_zSize / 2) + _zOffset * 2);

            _bottomNavMeshLink.startPoint = new Vector3(0, _upYSize, -((_zSize / 2) - _zOffset));
            _bottomNavMeshLink.endPoint = new Vector3(0, _downYSize, -((_zSize / 2) + _zOffset * 2));

            _rightNavMeshLink.startPoint = new Vector3((_xSize / 2) - _xOffset, _upYSize, 0);
            _rightNavMeshLink.endPoint = new Vector3((_xSize / 2) + _xOffset * 2, _downYSize, 0);

            _leftNavMeshLink.startPoint = new Vector3(-((_xSize / 2) - _xOffset), _upYSize, 0);
            _leftNavMeshLink.endPoint = new Vector3(-((_xSize / 2) + _xOffset * 2), _downYSize, 0);
        }
    }
}