using Charatcers.Player;
using ConstStrings;
using DI;
using Events;
using Helpers;
using Identifiers;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using UI.Canvases;
using Unity.AI.Navigation;
using UnityEngine;

namespace Managers.SceneManagers
{
    public class TD_SceneManager : SceneManagerBase, IDIDependent
    {
        [Header("Actors")]
        [SerializeField] private PlayerIdentifier _playerPrefab;

        [Header(HeaderStrings.components)]
        [SerializeField] private TD_EnemiesManager _enemiesManager;
        [SerializeField] private Transform _terrainReactableGridCenter;
        [SerializeField] private NavMeshSurface _navMeshSurface;

        [Header("Menus")]
        [SerializeField] private CanvasWindowBase _winMenu;
        [SerializeField] private CanvasWindowBase _loseMenu;

#if UNITY_EDITOR

        [SerializeField] private bool _isDebugMode = false;

#else

        private bool _isDebugMode => false;

#endif

        [Header(HeaderStrings.settings)]
        [SerializeField] private ReactorSpawnData[] _reactorSpawnDatas;
        [SerializeField] private int _xTerrainReactableSize;
        [SerializeField] private int _yTerrainReactableSize;
        [SerializeField] private int _xTerrainReactableGridSize;
        [SerializeField] private int _yTerrainReactableGridSize;

        [Header(HeaderStrings.di)]
        [DI(Event_DIStrings.onEnemyDied)][SerializeField] private EventWithOneParameters<IDamagable> _onEnemyDied;

        [Header("TerrainReactableParents")]
        [SerializeField] private Transform _bass_TerrainReactableParent;
        [SerializeField] private Transform _middle_TerrainReactableParent;
        [SerializeField] private Transform _high_TerrainReactableParent;

        [Header(HeaderStrings.debug)]
        [SerializeField] private List<PlayerSpawnPoint_Identifier> _playerSpawnPoints = new List<PlayerSpawnPoint_Identifier>();
        [SerializeField] private PlayerIdentifier _player;

        private void Awake()
        {

        }

        private async void OnEnable()
        {
            (this as IDIDependent).LoadDependencies();
            await InitializeAsync();

            SubscribeToEvents();
        }

        private void OnDisable()
        {
            UnsubscribeFromEvents();
        }

        public void SubscribeToEvents()
        {
            _onEnemyDied.AddListener(OnEnemyDied);

            _player.TryGet<PlayerHealth>().onDie += Lose;
        }

        public void UnsubscribeFromEvents()
        {
            _onEnemyDied.RemoveListener(OnEnemyDied);

            _player.TryGet<PlayerHealth>().onDie -= Lose;
        }

        public override async Awaitable InitializeAsync()
        {
            _playerSpawnPoints = FindObjectsByType<PlayerSpawnPoint_Identifier>(FindObjectsInactive.Include, FindObjectsSortMode.None).ToList();
            if (_playerSpawnPoints.Count > 0) _player = Instantiate(_playerPrefab, _playerSpawnPoints.GetRandom().transform.position, Quaternion.identity);

            for (int x = -_xTerrainReactableGridSize; x <= _xTerrainReactableGridSize; x++)
            {
                for (int z = -_yTerrainReactableGridSize; z <= _yTerrainReactableGridSize; z++)
                {
                    await AsyncHelper.NextFrame();

                    var reactableData = _reactorSpawnDatas.GetRandom();

                    var reactableInstance = Instantiate(reactableData.reactable_Identifiers.GetRandom(), reactableData.parent);

                    var reactablePositionX = _terrainReactableGridCenter.transform.position.x + (x * _xTerrainReactableSize);
                    var reactablePositionY = _terrainReactableGridCenter.transform.position.y;
                    var reactablePositionZ = _terrainReactableGridCenter.transform.position.z + (z * _yTerrainReactableSize);

                    var reactablePosition = new Vector3(reactablePositionX, reactablePositionY, reactablePositionZ);

                    reactableInstance.transform.localPosition = reactablePosition;
                    reactableInstance.transform.localEulerAngles = Vector3.zero;
                }
            }

            _navMeshSurface.BuildNavMesh();

            _enemiesManager.SpawnEnemies();

            await base.InitializeAsync();
        }

        private void OnEnemyDied(IDamagable enemy)
        {
            _enemiesManager.Respawn(enemy);
        }

        private void Lose()
        {
            Clear();

            _loseMenu?.Enable();
        }

        private void Win(Exit_Identifier identifier)
        {
            Clear();

            _winMenu?.Enable();
        }

        private void Clear()
        {

        }

        [Serializable]
        protected class ReactorSpawnData
        {
            public Transform parent;
            public TerrainReactable_Identifier[] reactable_Identifiers;
        }
    }
}