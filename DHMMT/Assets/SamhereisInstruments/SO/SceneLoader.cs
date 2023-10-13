using ConstStrings;
using DataClasses;
using DI;
using Helpers;
using Interfaces;
using SO.Lists;
using System;
using System.Threading.Tasks;
using UI.Windows;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace SamhereisTools
{
    [CreateAssetMenu(fileName = "Scene Loader", menuName = "Scriptables/Helpers/Scene Loader")]
    public class SceneLoader : ScriptableObject, IInitializable, IDIDependent
    {
        public readonly UnityEvent<AScene> onSceneStartLoading = new UnityEvent<AScene>();

        [SerializeField] private bool _loading = false;

        [Header(HeaderStrings.DI)]
        [DI(DIStrings.listOfAllScenes)][SerializeField] private ListOfAllScenes _listOfAllScenes;

        [Header(HeaderStrings.Debug)]
        [SerializeField] private string _lastLoadedSceneCode;

        public AScene lastLoadedScene { get; private set; }

        public void Initialize()
        {
            (this as IDIDependent).LoadDependencies();

            SetLastLoadedScene(_listOfAllScenes.GetScenes().Find(x => x.sceneCode == SceneManager.GetActiveScene().name));
        }

        public async Task LoadSceneAsync(AScene aScene, Action<float> onUpdate = null)
        {
            if (aScene == null) { return; }

            if (_loading == false)
            {
                _loading = true;

                onSceneStartLoading?.Invoke(aScene);

                var asyncOperation = SceneManager.LoadSceneAsync(aScene.sceneCode, LoadSceneMode.Single);

                while (asyncOperation.isDone == false)
                {
                    onUpdate?.Invoke(asyncOperation.progress);
                    await AsyncHelper.Delay();

                    if (asyncOperation.progress >= 0.9f)
                    {
                        Time.timeScale = 1;
                    }
                }

                SetLastLoadedScene(aScene);

                _loading = false;
            }
        }

        public static async Awaitable LoadSceneAsync(AScene scene, SceneLoader sceneLoader, LoadingMenu loadingMenu)
        {
            if (sceneLoader == null)
            {
                await Awaitable.FromAsyncOperation(SceneManager.LoadSceneAsync(scene.sceneCode));
            }
            else
            {
                if (loadingMenu != null)
                {
                    loadingMenu.SetProgress(0f);
                    loadingMenu.Enable();

                    await AsyncHelper.DelayFloat(1f);

                    await sceneLoader.LoadSceneAsync(scene, (percent) =>
                    {
                        loadingMenu.SetProgress(percent);
                    });
                }
                else
                {
                    await Awaitable.FromAsyncOperation(SceneManager.LoadSceneAsync(scene.sceneCode));
                }
            }
        }

        public void LoadSceneAdditively(int sceneId)
        {
            SceneManager.LoadSceneAsync(sceneId, LoadSceneMode.Additive);
        }

        public async Task LoadLastScene()
        {
            await LoadSceneAsync(lastLoadedScene);
        }

        public void SetLastLoadedScene(AScene aScene)
        {
            lastLoadedScene = aScene;

            if (lastLoadedScene != null)
            {
                _lastLoadedSceneCode = lastLoadedScene.sceneCode;
            }
        }
    }
}