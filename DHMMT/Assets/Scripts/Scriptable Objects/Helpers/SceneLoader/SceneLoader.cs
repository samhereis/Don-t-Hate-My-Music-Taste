using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Helpers
{
    [CreateAssetMenu(fileName = "ScemeLoader", menuName = "Scriptables/Helpers/Scene Loader")]

    public class SceneLoader : ScriptableObject
    {
        // Scene loader attachable on gameobject

        public readonly UnityEvent onSceneStartLoading = new UnityEvent();

        private static bool _loading = false;

        public void LoadScene(int index)
        {
            StartLoadScene(index);
        }

        private async void StartLoadScene(int sceneId)
        {
            if (_loading == false)
            {
                _loading = true;

                onSceneStartLoading?.Invoke();

                AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneId, LoadSceneMode.Single);
                asyncOperation.allowSceneActivation = false;

                while(asyncOperation.isDone == false)
                {
                    if (asyncOperation.progress == 0.9f)
                    {
                        _loading = false;

                        Time.timeScale = 1;

                        asyncOperation.allowSceneActivation = true;

                        break;

                    }

                    await ExtentionMethods.Delay();
                }
            }
        }

        public void LoadSceneAdditively(int sceneId)
        {
            SceneManager.LoadSceneAsync(sceneId, LoadSceneMode.Additive);
        }
    }
}
