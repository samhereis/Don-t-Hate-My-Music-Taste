using DI;
using Helpers;
using SO.Lists;
using SamhereisTools;
using UnityEngine;

namespace Managers.SceneManagers
{
    public class Init_SceneManager : SceneManagerBase
    {
        [Header("DI")]
        [DI(ConstStrings.DIStrings.sceneLoader)][SerializeField] private SceneLoader _sceneLoader;
        [DI(ConstStrings.DIStrings.listOfAllScenes)][SerializeField] private ListOfAllScenes _listOfAllScenes;

        private async void OnEnable()
        {
            Initialize();

            while (DependencyInjector.isGLoballyInjected == false)
            {
                await AsyncHelper.Delay(1f);
            }

            (this as IDIDependent).LoadDependencies();

            await _sceneLoader.LoadSceneAsync(_listOfAllScenes.mainMenu);
        }
    }
}