using ConstStrings;
using DI;
using Identifiers;
using IdentityCards;
using System.Collections.Generic;
using System.Linq;
using UI.Windows;
using UI.Windows.GameplayMenus;
using UnityEngine;

namespace GameStates.SceneManagers
{
    public class EFH_SceneManager : Scene_SceneManagerBase, IDIDependent
    {
        public List<ExitLocation_Identifier> exitLocations => _exitLocations;
        public List<PlayerSpawnPoint_Identifier> theLightLocations => _theLightLocations;
        public List<EnemyIdentityCard> enemiesToSpawnOnStart => _enemiesToSpawnOnStart;

        public TheLight_Identifier theLightPrefab => _theLightPrefab;
        public PlayerIdentifier playerPrefab => _playerPrefab;
        public Exit_Identifier exitPrefab => _exitPrefab;

        public EFH_GameplayMenu gameplayMenuPrefab => _gameplayMenuPrefab;
        public EFH_WinMenu winMenuPrefab => _winMenuPrefab;
        public EFH_LoseMenu loseMenuPrefab => _loseMenuPrefab;
        public PauseMenu pauseMenuPrefab => _pauseMenuPrefab;

        public int secondsUntillLoseWhileOutsideOfTheLight => _secondsUntillLoseWhileOutsideOfTheLight;
        public float lightRange => _lightRange;
        public LayerMask enemyNavmeshLayerMask => _enemyNavmeshLayerMask;

        [Header("Actors")]
        [SerializeField] private TheLight_Identifier _theLightPrefab;
        [SerializeField] private PlayerIdentifier _playerPrefab;
        [SerializeField] private Exit_Identifier _exitPrefab;

        [Header(HeaderStrings.Components)]
        [SerializeField] private Light _directionalLight;

        [Header("Menus")]
        [SerializeField] private EFH_GameplayMenu _gameplayMenuPrefab;
        [SerializeField] private EFH_WinMenu _winMenuPrefab;
        [SerializeField] private EFH_LoseMenu _loseMenuPrefab;
        [SerializeField] private PauseMenu _pauseMenuPrefab;

        [Header(HeaderStrings.Settings)]
        [SerializeField] private float _directionalLightIntencity = 0.01f;
        [SerializeField] private int _secondsUntillLoseWhileOutsideOfTheLight = 25;
        [SerializeField] private float _lightRange = 10;
        [SerializeField] private LayerMask _enemyNavmeshLayerMask;


        [SerializeField] private List<ExitLocation_Identifier> _exitLocations = new List<ExitLocation_Identifier>();
        [SerializeField] private List<PlayerSpawnPoint_Identifier> _theLightLocations = new List<PlayerSpawnPoint_Identifier>();
        [SerializeField] private List<EnemyIdentityCard> _enemiesToSpawnOnStart = new List<EnemyIdentityCard>();

        public override void Initialize()
        {
            (this as IDIDependent).LoadDependencies();

            _exitLocations = FindObjectsByType<ExitLocation_Identifier>(FindObjectsInactive.Include, FindObjectsSortMode.None).ToList();
            _theLightLocations = FindObjectsByType<PlayerSpawnPoint_Identifier>(FindObjectsInactive.Include, FindObjectsSortMode.None).ToList();

            if (_directionalLight == null) { _directionalLight = FindFirstObjectByType<Light>(FindObjectsInactive.Include); }
            if (_isDebugMode == false) { _directionalLight.intensity = _directionalLightIntencity; }

            isInitialized = true;
        }

        public override void Clear()
        {

        }
    }
}