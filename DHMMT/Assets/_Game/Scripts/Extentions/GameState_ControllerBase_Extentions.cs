using DataClasses;
using DependencyInjection;
using Servies;
using SO;
using UI.Windows;
using UnityEngine;

namespace GameState
{
    public static class GameState_ControllerBase_Extentions
    {
        private static SceneLoader _sceneLoader;
        private static ListOfAllViews _listOfAllViews;

        public static async Awaitable<LoadingMenu_Extended> LoadSceneWithLoadingMenu(
            this GameState_ControllerBase gameState_ControllerBase,
            AScene aScene)
        {
            if (_sceneLoader == null) { _sceneLoader = DependencyContext.diBox.Get<SceneLoader>(); }
            if (_listOfAllViews == null) { _listOfAllViews = DependencyContext.diBox.Get<ListOfAllViews>(); }

            LoadingMenu_Extended loadingMenuPrefab = await _listOfAllViews?.GetViewAsync<LoadingMenu_Extended>();
            LoadingMenu_Extended loadingMenu = null;
            if (loadingMenuPrefab != null) 
            { 
                loadingMenu = Object.Instantiate(loadingMenuPrefab);
                loadingMenu.Enable();
                Object.DontDestroyOnLoad(loadingMenu.gameObject);
            }

            await _sceneLoader.LoadSceneAsync(aScene, loadingMenu, (loadUpdate) =>
            {
                Debug.Log(loadUpdate);
            });

            return loadingMenu;
        }
    }
}