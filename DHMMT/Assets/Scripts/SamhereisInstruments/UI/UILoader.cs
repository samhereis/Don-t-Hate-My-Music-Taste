using UnityEngine;
using UnityEngine.SceneManagement;

namespace Samhereis.Helpers
{
    public class UILoader : MonoBehaviour
    {
        [Header("Helpers")]
        [SerializeField] private SceneLoader _sceneLoader;

        private void Awake()
        {
            if(_sceneLoader == null)
            {
                AddressablesHelper.LoadAndDo<SceneLoader>("SceneLoader", (x) =>
                {
                    _sceneLoader = x;
                    LoadUI();
                });
            }
            else LoadUI();
        }

        private async void LoadUI()
        {
            if (SceneManager.GetSceneByBuildIndex(1).isLoaded == false)
            {
                await AsyncHelper.Delay(() => _sceneLoader.LoadSceneAdditively(1));
            }
        }
    }
}
