using ConstStrings;
using DataClasses;
using DependencyInjection;
using GameState;
using Identifiers;
using Interfaces;
using Music;
using Observables;

namespace GameStates.SceneManagers
{
    public class EFH_GameState_Model : GameState_ModelBase, INeedDependencyInjection
    {
        [Inject] public EFH_Scene sceneManager;

        [Inject] public PlayingMusicData playingMusicData;

        [Inject(DataSignal_ConstStrings.onGameplayStatusChaned)] public DataSignal<GameplayStatus> onGameplayStatusChanged;
        [Inject(DataSignal_ConstStrings.onExitFound)] public DataSignal<Exit_Identifier> onExitFound;
        [Inject(DataSignal_ConstStrings.onEnemyDied)] public DataSignal<IDamagable> onEnemyDied;

        public Exit_Identifier exit;
        public TheLight_Identifier theLight;
        public PlayerIdentifier playerIdentifier;

        public override void Initialize()
        {
            base.Initialize();

            DependencyContext.InjectDependencies(this);
            sceneManager.Initialize();

            exit = sceneManager.SpawnExit();
            theLight = sceneManager.SpawnTheLight();
            playerIdentifier = sceneManager.SpawnPlayer(theLight.transform.position);

            isInitialized = true;
        }
    }
}