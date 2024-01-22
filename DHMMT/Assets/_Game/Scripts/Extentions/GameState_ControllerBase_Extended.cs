using DataClasses;
using DependencyInjection;
using ErtenGamesInstrumentals.DataClasses;
using Services;
using UI.Windows;
using UnityEngine;

namespace GameState
{
    public static class GameState_ControllerBase_Extended
    {
        private static SceneLoader _sceneLoader;

        public static async Awaitable<LoadingMenu> LoadSceneWithLoadingMenu(this GameState_ControllerBase gameState_ControllerBase, AScene aScene)
        {
            if (_sceneLoader == null) { DependencyContext.diBox.Get<SceneLoader>(); }

            var loadingMenuReference = DependencyContext.diBox.Get<PrefabReference<LoadingMenu>>();

            LoadingMenu loadingMenu = null;

            if (loadingMenuReference != null)
            {
                loadingMenu = Object.Instantiate(await loadingMenuReference.GetAssetAsync());
                if (loadingMenuReference != null) { Object.DontDestroyOnLoad(loadingMenu.gameObject); };
            }

            await _sceneLoader.LoadSceneAsync(aScene, loadingMenu);

            return loadingMenu;
        }
    }
}