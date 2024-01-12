using DependencyInjection;
using Identifiers;
using IdentityCards;
using Interfaces;
using System.Collections.Generic;
using System.Linq;
using UI.Windows;
using UI.Windows.GameplayMenus;
using UnityEngine;

namespace GameStates.SceneManagers
{
    public class EFH_SceneManager : MonoBehaviour, INeedDependencyInjection, IInitializable
    {
        public List<ExitLocation_Identifier> exitLocations => _exitLocations;
        public List<PlayerSpawnPoint_Identifier> theLightLocations => _theLightLocations;
        public List<EnemyIdentityCard> enemiesToSpawnOnStart => _enemiesToSpawnOnStart;

        [Header("Actors")]
        [field: SerializeField] public TheLight_Identifier theLightPrefab;
        [field: SerializeField] public PlayerIdentifier playerPrefab;
        [field: SerializeField] public Exit_Identifier exitPrefab;

        [Header("Components")]
        [field: SerializeField] private Light directionalLight;

        [Header("Menus")]
        [field: SerializeField] public EFH_GameplayMenu gameplayMenuPrefab;
        [field: SerializeField] public EFH_WinMenu winMenuPrefab;
        [field: SerializeField] public EFH_LoseMenu loseMenuPrefab;
        [field: SerializeField] public PauseMenu pauseMenuPrefab;

        [Header("Settings")]
        [field: SerializeField] public bool isDebugMode;
        [field: SerializeField] public float directionalLightIntencity = 0.01f;
        [field: SerializeField] public int secondsUntillLoseWhileOutsideOfTheLight = 25;
        [field: SerializeField] public float lightRange = 10;
        [field: SerializeField] public LayerMask enemyNavmeshLayerMask;

        [SerializeField] private List<ExitLocation_Identifier> _exitLocations = new List<ExitLocation_Identifier>();
        [SerializeField] private List<PlayerSpawnPoint_Identifier> _theLightLocations = new List<PlayerSpawnPoint_Identifier>();
        [SerializeField] private List<EnemyIdentityCard> _enemiesToSpawnOnStart = new List<EnemyIdentityCard>();

        public bool isInitialized { get; private set; }

        public void Initialize()
        {
            DependencyContext.diBox.InjectDataTo(this);

            _exitLocations = FindObjectsByType<ExitLocation_Identifier>(FindObjectsInactive.Include, FindObjectsSortMode.None).ToList();
            _theLightLocations = FindObjectsByType<PlayerSpawnPoint_Identifier>(FindObjectsInactive.Include, FindObjectsSortMode.None).ToList();

            if (directionalLight == null) { directionalLight = FindFirstObjectByType<Light>(FindObjectsInactive.Include); }
            if (isDebugMode == false) { directionalLight.intensity = directionalLightIntencity; }

            isInitialized = true;
        }

        public void Clear()
        {

        }
    }
}