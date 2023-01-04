using Helpers;
using Tools;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class UILoader : MonoBehaviour
    {
        [Header("Helpers")]
        [SerializeField] private SceneLoader _sceneLoader;

        private void Awake()
        {
            if (SceneManager.GetSceneByBuildIndex(1).isLoaded == false)
            {
                _sceneLoader.LoadSceneAdditively(1);
            }
        }
    }
}
