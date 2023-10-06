using Charatcers.Player;
using ConstStrings;
using DI;
using Events;
using Identifiers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UI.Canvases;
using UI.Windows.GameplayMenus;
using UnityEngine;

namespace Managers.SceneManagers
{
    public class EscapeFromHelpers_SceneManager : SceneManagerBase, IDIDependent
    {
        [SerializeField] private List<ExitLocation_Identifier> _exitLocations = new List<ExitLocation_Identifier>();
        [SerializeField] private List<TheLightLocation_Identifier> _theLightLocations = new List<TheLightLocation_Identifier>();

        [Header("Actors")]
        [SerializeField] private Exit_Identifier _exitPrefab;
        [SerializeField] private TheLight_Identifier _theLightPrefab;
        [SerializeField] private PlayerIdentifier _playerPrefab;

        [Header(HeaderStrings.components)]
        [SerializeField] private EscapeFromHaters_EnemiesManager _enemiesManager;
        [SerializeField] protected EFH_GameplayMenu _gameplayMenu;

        [Header("Menus")]
        [SerializeField] private CanvasWindowBase _winMenu;
        [SerializeField] private CanvasWindowBase _loseMenu;

        [Header(HeaderStrings.settings)]
        [SerializeField] private int _secondsUntillLoseWhileOutsideOfTheLight = 25;
        [SerializeField] private float _lightRange = 10;

#if UNITY_EDITOR

        [SerializeField] private bool _isDebugMode = false;

#else

        private bool _isDebugMode => false;

#endif

        [Header(HeaderStrings.di)]
        [DI(Event_DIStrings.onExitFound)][SerializeField] private EventWithOneParameters<Exit_Identifier> _onExitFound;

        [Header(HeaderStrings.debug)]
        [SerializeField] private Exit_Identifier _exit;
        [SerializeField] private TheLight_Identifier _theLight;
        [SerializeField] private PlayerIdentifier _player;

        private Coroutine _checkForIsWithinTheLight_Coroutine;

        private void Awake()
        {
            _gameplayMenu = GetComponentInChildren<EFH_GameplayMenu>(true);
        }

        private void Start()
        {
            (this as IDIDependent).LoadDependencies();

            _onExitFound.AddListener(Win);
        }

        private void OnDestroy()
        {
            _onExitFound.RemoveListener(Win);
        }

        private void OnEnable()
        {
            Initialize();
        }

        public override void Initialize()
        {
            base.Initialize();

            _exitLocations = FindObjectsOfType<ExitLocation_Identifier>().ToList();
            _theLightLocations = FindObjectsOfType<TheLightLocation_Identifier>().ToList();

            if (_exitLocations.Count > 0)
            {
                var randomExitLocation = _exitLocations[Random.Range(0, _exitLocations.Count)];

                _exit = Instantiate(_exitPrefab, randomExitLocation.transform.position, Quaternion.identity);
            }

            if (_theLightLocations.Count > 0)
            {
                var randomTheLightLocation = _theLightLocations[Random.Range(0, _theLightLocations.Count)];

                if (_isDebugMode == false) { _theLight = Instantiate(_theLightPrefab, randomTheLightLocation.transform.position, Quaternion.identity); }
                _player = Instantiate(_playerPrefab, randomTheLightLocation.transform.position, Quaternion.identity);
            }

            if (_isDebugMode == false) { _enemiesManager.SpawnEnemies(); }

            _player.TryGet<PlayerHealth>().onDie += Lose;

            _checkForIsWithinTheLight_Coroutine = StartCoroutine(CheckForIsWithinTheLight());
        }

        private IEnumerator CheckForIsWithinTheLight()
        {
            if (_isDebugMode == true) { yield break; }

            var wait = new WaitForSecondsRealtime(1f);
            int currentSeconds = _secondsUntillLoseWhileOutsideOfTheLight;

            while (true)
            {
                if (Vector3.Distance(_theLight.transform.position, _player.transform.position) > _lightRange)
                {
                    OnPlayerIsOutsideLightRange(ref currentSeconds);
                }
                else
                {
                    OnPlayerIsIntsideLightRange(ref currentSeconds);
                }

                yield return wait;
            }
        }

        private void OnPlayerIsOutsideLightRange(ref int currentSeconds)
        {
            currentSeconds--;

            if (_gameplayMenu.stayUnderTheLight_Popup.isEnabled == false)
            {
                _gameplayMenu.stayUnderTheLight_Popup.Enable();
            }

            _gameplayMenu.stayUnderTheLight_Popup.SetSeconds(currentSeconds);

            if (currentSeconds <= 0)
            {
                Lose();
            }
        }

        private void OnPlayerIsIntsideLightRange(ref int currentSeconds)
        {
            currentSeconds = _secondsUntillLoseWhileOutsideOfTheLight;

            if (_gameplayMenu.stayUnderTheLight_Popup.isEnabled == true)
            {
                _gameplayMenu.stayUnderTheLight_Popup.Disable();
            }
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
            StopCoroutine(_checkForIsWithinTheLight_Coroutine);
        }
    }
}