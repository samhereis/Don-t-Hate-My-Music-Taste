using ConstStrings;
using DataClasses;
using GameState;
using Observables;
using Services;
using Servies;

namespace DependencyInjection
{
    public class Global_DependencyInstaller : DependencyInstallerBase
    {
        public override void Inject()
        {
            base.Inject();

            InjectSerices();
            InjectDataSignals();

            DependencyContext.diBox.Add<IGameStateChanger>(new SimpleGameStatesChanger(), asTypeProvided: true);
        }

        public override void Clear()
        {
            base.Clear();

            ClearSerices();
            ClearDataSignals();

            DependencyContext.diBox.Remove<IGameStateChanger>();
        }

        private void InjectSerices()
        {
            PlayerInputService playerInputService = new PlayerInputService();

            DependencyContext.diBox.Add<SceneLoader>(new SceneLoader());
            DependencyContext.diBox.Add<PlayerInputService>(playerInputService);
            DependencyContext.diBox.Add<InputsService>(playerInputService, asTypeProvided: true);
            DependencyContext.diBox.Add<LazyUpdator_Service>(new LazyUpdator_Service());
            DependencyContext.diBox.Add<GameSaveService>(new GameSaveService());
        }

        private void ClearSerices()
        {
            DependencyContext.diBox.Remove<SceneLoader>();
            DependencyContext.diBox.Remove<PlayerInputService>();
            DependencyContext.diBox.Remove<InputsService>();
            DependencyContext.diBox.Remove<LazyUpdator_Service>();
            DependencyContext.diBox.Remove<GameSaveService>();
        }

        private void InjectDataSignals()
        {
            DependencyContext.diBox.Add<DataSignal<AScene_Extended>>(
                new DataSignal<AScene_Extended>(DataSignal_ConstStrings.onASceneSelected),
                DataSignal_ConstStrings.onASceneSelected);

            DependencyContext.diBox.Add<DataSignal<AScene_Extended>>(
                new DataSignal<AScene_Extended>(DataSignal_ConstStrings.onASceneLoadRequested),
                DataSignal_ConstStrings.onASceneLoadRequested);
        }

        private void ClearDataSignals()
        {
            DependencyContext.diBox.Remove<DataSignal<AScene_Extended>>(DataSignal_ConstStrings.onASceneSelected);
            DependencyContext.diBox.Remove<DataSignal<AScene_Extended>>(DataSignal_ConstStrings.onASceneLoadRequested);
        }
    }
}