using GameStates;
using UnityEngine;

namespace DependencyInjection
{
    public class MainMenu_DependencyInstaller : DependencyInstallerBase
    {
        [SerializeField] private MainMenu_Scene _sceneManager;

        public override void Inject()
        {
            base.Inject();

            DependencyContext.diBox.Add<MainMenu_Scene>(_sceneManager);
        }

        public override void Clear()
        {
            base.Clear();

            DependencyContext.diBox.Remove<MainMenu_Scene>();
        }
    }
}