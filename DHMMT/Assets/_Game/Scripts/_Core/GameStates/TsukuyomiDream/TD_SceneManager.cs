using DependencyInjection;
using Helpers;
using Identifiers;
using IdentityCards;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UI.Windows;
using UI.Windows.GameplayMenus;
using Unity.AI.Navigation;
using UnityEngine;

namespace GameStates.SceneManagers
{
    public class TD_SceneManager : MonoBehaviour, INeedDependencyInjection
    {
        public TD_GameplayMenu modeTDGameplayMenuPrefab => _modeTDGameplayMenuPrefab;
        public TD_LoseMenu modeLoseMenuPrefab => _modeLoseMenuPrefab;
        public PauseMenu pauseMenuPrefab => _pauseMenuPrefab;
        public PlayerIdentifier playerPrefab => _playerPrefab;
        public List<EnemyIdentityCard> enemiesToSpawnOnStart => _enemiesToSpawnOnStart;

        public PlayerSpawnPoint_Identifier[] playerSpawnPoints => _playerSpawnPoints;
        public EnemySpawnPoint_Identifier[] enemySpawnPoint_Identifiers => _enemySpawnPoint_Identifiers;

        public int secondsToGiveOnEnemyDie => _secondsToGiveOnEnemyDie;

        [Header("Prefabs")]
        [SerializeField] private PlayerIdentifier _playerPrefab;
        [SerializeField] private TD_GameplayMenu _modeTDGameplayMenuPrefab;
        [SerializeField] private TD_LoseMenu _modeLoseMenuPrefab;
        [SerializeField] private PauseMenu _pauseMenuPrefab;

        [Header("Components")]
        [SerializeField] private Transform _terrainReactableGridCenter;
        [SerializeField] private NavMeshSurface _navMeshSurface;

        [Header("Settings")]
        [SerializeField] private int _secondsToGiveOnEnemyDie = 25;
        [SerializeField] private Vector2Int _tReactableSize;
        [SerializeField] private Vector2Int _terrainReactableGridSize;
        [SerializeField] private LayerMask _enemyNavmeshLayerMask;

        [SerializeField] private ReactorSpawnData[] _reactorSpawnDatas;
        [SerializeField] private List<EnemyIdentityCard> _enemiesToSpawnOnStart = new List<EnemyIdentityCard>();
        [SerializeField] private PlayerSpawnPoint_Identifier[] _playerSpawnPoints;
        [SerializeField] private EnemySpawnPoint_Identifier[] _enemySpawnPoint_Identifiers;

        public bool isInitialized { get; private set; }

        public async void Initialize()
        {
            DependencyContext.diBox.InjectDataTo(this);

            await SpawnTerrainReactables();

            if (_navMeshSurface.navMeshData == null) { _navMeshSurface.BuildNavMesh(); }

            if (_playerSpawnPoints.Length == 0) { _playerSpawnPoints = FindObjectsByType<PlayerSpawnPoint_Identifier>(FindObjectsInactive.Include, FindObjectsSortMode.None); }
            if (_enemySpawnPoint_Identifiers.Length == 0) { _enemySpawnPoint_Identifiers = FindObjectsByType<EnemySpawnPoint_Identifier>(FindObjectsInactive.Include, FindObjectsSortMode.None); }

            isInitialized = true;
        }

        [ContextMenu(nameof(SpawnTerrainReactables_Editor))]
        public async void SpawnTerrainReactables_Editor()
        {
            ClearTerrainReactables();

            for (int x = -_terrainReactableGridSize.x; x <= _terrainReactableGridSize.x; x++)
            {
                for (int z = -_terrainReactableGridSize.y; z <= _terrainReactableGridSize.y; z++)
                {
                    await Task.Yield();

                    var reactableData = _reactorSpawnDatas.GetRandom();

                    var reactableInstance = Instantiate(reactableData.reactable_Identifiers.GetRandom(), reactableData.parent);

                    var reactablePositionX = _terrainReactableGridCenter.transform.position.x + (x * _tReactableSize.x);
                    var reactablePositionY = _terrainReactableGridCenter.transform.position.y;
                    var reactablePositionZ = _terrainReactableGridCenter.transform.position.z + (z * _tReactableSize.y);

                    var reactablePosition = new Vector3(reactablePositionX, reactablePositionY, reactablePositionZ);

                    reactableInstance.transform.localPosition = reactablePosition;
                    reactableInstance.transform.localEulerAngles = Vector3.zero;
                }
            }
        }

        private async Awaitable SpawnTerrainReactables()
        {
            for (int x = -_terrainReactableGridSize.x; x <= _terrainReactableGridSize.x; x++)
            {
                for (int z = -_terrainReactableGridSize.y; z <= _terrainReactableGridSize.y; z++)
                {
                    await AsyncHelper.NextFrame();

                    var reactableData = _reactorSpawnDatas.GetRandom();

                    var reactableInstance = Instantiate(reactableData.reactable_Identifiers.GetRandom(), reactableData.parent);

                    var reactablePositionX = _terrainReactableGridCenter.transform.position.x + (x * _tReactableSize.x);
                    var reactablePositionY = _terrainReactableGridCenter.transform.position.y;
                    var reactablePositionZ = _terrainReactableGridCenter.transform.position.z + (z * _tReactableSize.y);

                    var reactablePosition = new Vector3(reactablePositionX, reactablePositionY, reactablePositionZ);

                    reactableInstance.transform.localPosition = reactablePosition;
                    reactableInstance.transform.localEulerAngles = Vector3.zero;
                }
            }
        }

        public void Dispose()
        {
            isInitialized = false;
        }

        [ContextMenu(nameof(ClearTerrainReactables))]
        public void ClearTerrainReactables()
        {
            foreach (var terrainReactable in FindObjectsByType<TerrainReactable_Identifier>(FindObjectsInactive.Include, FindObjectsSortMode.None))
            {
                DestroyImmediate(terrainReactable.gameObject);
            }
        }

        [Serializable]
        protected class ReactorSpawnData
        {
            public Transform parent;
            public TerrainReactable_Identifier[] reactable_Identifiers;
        }
    }
}