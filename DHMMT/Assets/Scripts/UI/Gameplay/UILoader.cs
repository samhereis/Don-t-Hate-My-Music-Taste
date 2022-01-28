using UnityEngine;
using UnityEngine.SceneManagement;

namespace Helpers.UI
{
    public class UILoader : MonoBehaviour
    {
        // Usually used to load UI

        [Header("Helpers")]
        [SerializeField] private SceneLoader _sceneLoader;

        private async void Awake()
        {
            if (SceneManager.GetSceneByBuildIndex(1).isLoaded == false)
            {
                await AsyncHelper.Delay();

                _sceneLoader.LoadSceneAdditively(1);
            }
        }
    }
}
