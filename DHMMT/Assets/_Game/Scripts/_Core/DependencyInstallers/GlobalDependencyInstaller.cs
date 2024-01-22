using ErtenGamesInstrumentals.DataClasses;
using GameState;
using Services;
using UI.Windows;
using UnityEngine;

namespace DependencyInjection
{
    public class GlobalDependencyInstaller : DependencyInstallerBase
    {
        [SerializeField] private PrefabReference<LoadingMenu> _loadingMenu;

        public override void Inject()
        {
            base.Inject();

            AddWithAutoDelete<SceneLoader>(new SceneLoader());
            AddWithAutoDelete<PlayerInputService>(new PlayerInputService());
            AddWithAutoDelete<LazyUpdator_Service>(new LazyUpdator_Service());
            AddWithAutoDelete<GameSaveService>(new GameSaveService());

            AddWithAutoDelete<PrefabReference<LoadingMenu>>(_loadingMenu);

            DependencyContext.diBox.Add<IGameStateChanger>(new SimpleGameStatesChanger(), asTypeProvided: true);
        }

        public override void Clear()
        {
            base.Clear();

            DependencyContext.diBox.Remove<IGameStateChanger>();
        }
    }
}