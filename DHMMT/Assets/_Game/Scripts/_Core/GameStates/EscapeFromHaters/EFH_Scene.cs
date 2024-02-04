using DependencyInjection;
using Helpers;
using Identifiers;
using IdentityCards;
using Interfaces;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using UI.Windows;
using UI.Windows.GameplayMenus;
using UnityEngine;

namespace GameStates.SceneManagers
{
    public class EFH_Scene : MonoBehaviour, INeedDependencyInjection, IInitializable
    {
        public List<ExitLocation_Identifier> exitLocations => _exitLocations;
        public List<PlayerSpawnPoint_Identifier> theLightLocations => _theLightLocations;
        public List<EnemyIdentityCard> enemiesToSpawnOnStart => _enemiesToSpawnOnStart;

        [field: FoldoutGroup("Prefabs"), SerializeField] public TheLight_Identifier theLightPrefab;
        [field: FoldoutGroup("Prefabs"), SerializeField] public Exit_Identifier exitPrefab;
        [field: FoldoutGroup("Prefabs"), SerializeField] public PlayerIdentifier playerPrefab;

        [field: FoldoutGroup("Components"), SerializeField] private Light directionalLight;


        [field: FoldoutGroup("Menus"), SerializeField] public EFH_GameplayMenu gameplayMenuPrefab;
        [field: FoldoutGroup("Menus"), SerializeField] public EFH_WinMenu winMenuPrefab;
        [field: FoldoutGroup("Menus"), SerializeField] public EFH_LoseMenu loseMenuPrefab;
        [field: FoldoutGroup("Menus"), SerializeField] public PauseMenu pauseMenuPrefab;

        [field: FoldoutGroup("Settings"), SerializeField] public bool isDebugMode;
        [field: FoldoutGroup("Settings"), SerializeField] public float directionalLightIntencity = 0.01f;
        [field: FoldoutGroup("Settings"), SerializeField] public int secondsUntillLoseWhileOutsideOfTheLight = 25;
        [field: FoldoutGroup("Settings"), SerializeField] public float lightRange = 10;
        [field: FoldoutGroup("Settings"), SerializeField] public LayerMask enemyNavmeshLayerMask;

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

        public Exit_Identifier SpawnExit()
        {
            Exit_Identifier exit = null;

            if (exitLocations.Count > 0)
            {
                var randomExitLocation = exitLocations[Random.Range(0, exitLocations.Count)];

                exit = Object.Instantiate(exitPrefab,
                    randomExitLocation.transform.position,
                    Quaternion.identity);
            }

            return exit;
        }

        public TheLight_Identifier SpawnTheLight()
        {
            TheLight_Identifier theLight = null;

            if (theLightLocations.Count > 0)
            {
                var randomTheLightLocation = theLightLocations.GetRandom();

                if (isDebugMode == false)
                {
                    theLight = Object.Instantiate(theLightPrefab,
                        randomTheLightLocation.transform.position,
                        Quaternion.identity);
                }
            }

            return theLight;
        }

        public PlayerIdentifier SpawnPlayer(Vector3 position)
        {
            return Instantiate(playerPrefab, position, Quaternion.identity);
        }

        public void Clear()
        {

        }
    }
}