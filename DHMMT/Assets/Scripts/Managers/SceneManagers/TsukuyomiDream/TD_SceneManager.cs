using Charatcers.Player;
using ConstStrings;
using DI;
using Events;
using Helpers;
using Identifiers;
using Interfaces;
using Music;
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
        [SerializeField] private int _xTerrainReactableSize;
        [SerializeField] private int _yTerrainReactableSize;
        [SerializeField] private int _xTerrainReactableGridSize;
        [SerializeField] private int _yTerrainReactableGridSize;
        [SerializeField] private int _secondsToGiveOnEnemyDie = 25;

        [Header(HeaderStrings.DI)]
        [DI(Event_DIStrings.onEnemyDied)][SerializeField] private EventWithOneParameters<IDamagable> _onEnemyDied;
        [DI(DIStrings.playingMusicData)][SerializeField] private PlayingMusicData _playingMusicData;

        [Header("TerrainReactableParents")]
        [SerializeField] private Transform _bass_TerrainReactableParent;
        [SerializeField] private Transform _middle_TerrainReactableParent;
        [SerializeField] private Transform _high_TerrainReactableParent;

        [Header(HeaderStrings.Debug)]
        [SerializeField] private List<PlayerSpawnPoint_Identifier> _playerSpawnPoints = new List<PlayerSpawnPoint_Identifier>();
        [SerializeField] private PlayerIdentifier _player;

        private void Awake()
        {
            if (_gameplayMenu == null) { _gameplayMenu = GetComponent<TD_GameplayMenu>(); }
        }

        private async void OnEnable()
        {
            (this as IDIDependent).LoadDependencies();

            await SpawnTerrainReactables();
            //_navMeshSurface.BuildNavMesh();
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

            _gameplayMenu.gameplayMenu.onOpen += PlayMusic;
            _gameplayMenu.gameplayMenu.onClose += StopMusic;
            _gameplayMenu.onTimerOver += Lose;
        }

        public void UnsubscribeFromEvents()
        {
            _onEnemyDied.RemoveListener(SpawnEnemy);
            _player.TryGet<PlayerHealth>().onDie -= SpawnPlayer;

            _gameplayMenu.gameplayMenu.onOpen -= PlayMusic;
            _gameplayMenu.gameplayMenu.onClose -= StopMusic;
            _gameplayMenu.onTimerOver -= Lose;
        }

        public override async Awaitable InitializeAsync()
        {
            await base.InitializeAsync();
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
        }

        private void Lose()
        {
            Clear();

            _loseMenu?.Enable();
        }

        private void PlayMusic()
        {
            _playingMusicData.SetActive(true);
        }

        private void StopMusic()
        {
            _playingMusicData.SetActive(false);
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