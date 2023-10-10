using Charatcers.Player;
using ConstStrings;
using DataClasses;
using DI;
using Events;
using Helpers;
using Identifiers;
using Interfaces;
using Music;
using SamhereisTools;
using SO.Lists;
using System;
using System.Collections.Generic;
using System.Linq;
using UI.Canvases;
using UI.Windows.GameplayMenus;
using Unity.AI.Navigation;
using UnityEngine;

namespace Managers.SceneManagers
{
    public class TD_SceneManager : SceneManagerBase, IDIDependent
    {
        [Header("Actors")]
        [SerializeField] private PlayerIdentifier _playerPrefab;

        [Header(HeaderStrings.Components)]
        [SerializeField] private TD_EnemiesManager _enemiesManager;
        [SerializeField] private Transform _terrainReactableGridCenter;
        [SerializeField] private NavMeshSurface _navMeshSurface;

        [Header("Menus")]
        [SerializeField] private TD_GameplayMenu _gameplayMenu;
        [SerializeField] private CanvasWindowBase _loseMenu;

#if UNITY_EDITOR

        [SerializeField] private bool _isDebugMode = false;

#else

        private bool _isDebugMode => false;

#endif

        [Header(HeaderStrings.Settings)]
        [SerializeField] private ReactorSpawnData[] _reactorSpawnDatas;
        [SerializeField] private Vector2Int _tReactableSize;
        [SerializeField] private Vector2Int _terrainReactableGridSize;
        [SerializeField] private int _secondsToGiveOnEnemyDie = 25;

        [Header(HeaderStrings.DI)]
        [DI(Event_DIStrings.onEnemyDied)][SerializeField] private EventWithOneParameters<IDamagable> _onEnemyDied;
        [DI(DIStrings.playingMusicData)][SerializeField] private PlayingMusicData _playingMusicData;
        [DI(DIStrings.gameSaveManager)][SerializeField] private GameSaveManager _gameSaveManager;
        [DI(DIStrings.sceneLoader)][SerializeField] private SceneLoader _sceneLoader;
        [DI(DIStrings.listOfAllScenes)][SerializeField] private ListOfAllScenes _listOfAllScenes;

        [Header(HeaderStrings.Debug)]
        [SerializeField] private List<PlayerSpawnPoint_Identifier> _playerSpawnPoints = new List<PlayerSpawnPoint_Identifier>();
        [SerializeField] private PlayerIdentifier _player;

        private void Awake()
        {
            if (_gameplayMenu == null) { _gameplayMenu = GetComponentInChildren<TD_GameplayMenu>(true); }
        }

        private async void OnEnable()
        {
            (this as IDIDependent).LoadDependencies();

            await SpawnTerrainReactables();
            if (_navMeshSurface.navMeshData == null) { _navMeshSurface.BuildNavMesh(); }
            SpawnPlayer();
            _enemiesManager.SpawnEnemies();

            SubscribeToEvents();

            await InitializeAsync();
        }

        private void OnDisable()
        {
            UnsubscribeFromEvents();
        }

        public void SubscribeToEvents()
        {
            _onEnemyDied.AddListener(SpawnEnemy);
            _player.TryGet<PlayerHealth>().onDie += SpawnPlayer;

            _gameplayMenu.gameplayMenu.onEnable += UnPauseMusic;
            _gameplayMenu.gameplayMenu.onDisable += PauseMusic;
            _gameplayMenu.onTimerOver += Lose;
        }

        public void UnsubscribeFromEvents()
        {
            _onEnemyDied.RemoveListener(SpawnEnemy);
            _player.TryGet<PlayerHealth>().onDie -= SpawnPlayer;

            _gameplayMenu.gameplayMenu.onEnable -= UnPauseMusic;
            _gameplayMenu.gameplayMenu.onDisable -= PauseMusic;
            _gameplayMenu.onTimerOver -= Lose;
        }

        public override async Awaitable InitializeAsync()
        {
            await base.InitializeAsync();

            _playingMusicData.SetActive(true, true);
        }

        private void SpawnEnemy(IDamagable enemy)
        {
            _gameplayMenu.AddSeconds(_secondsToGiveOnEnemyDie);
            _enemiesManager.Respawn(enemy);
        }

        private void SpawnPlayer()
        {
            _playerSpawnPoints = FindObjectsByType<PlayerSpawnPoint_Identifier>(FindObjectsInactive.Include, FindObjectsSortMode.None).ToList();
            if (_playerSpawnPoints.Count > 0) _player = Instantiate(_playerPrefab, _playerSpawnPoints.GetRandom().transform.position, Quaternion.identity);
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

        private void Lose()
        {
            Clear();

            var currentScene = _sceneLoader.lastLoadedScene;
            var modeTDSave = _gameSaveManager.modeTDSaves;
            var modeTDSaveUnit = modeTDSave.tD_Saves.Find(x => x.sceneName == currentScene.sceneCode);

            if (modeTDSaveUnit == null)
            {
                modeTDSaveUnit = new TD_SaveUnit();
                modeTDSaveUnit.sceneName = currentScene.sceneCode;

                modeTDSave.tD_Saves.SafeAdd(modeTDSaveUnit);
            }

            if (modeTDSaveUnit != null)
            {
                modeTDSaveUnit.record = _gameplayMenu.currentDuration;
            }

            _gameSaveManager.Save(_gameSaveManager.modeTDSaves);
            _loseMenu?.Enable();
        }

        private void UnPauseMusic()
        {
            _playingMusicData.PauseMusic(false);
        }

        private void PauseMusic()
        {
            _playingMusicData.PauseMusic(true);
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