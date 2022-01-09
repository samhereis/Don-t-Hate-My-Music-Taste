using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoadController
{
    // Scene load methods

    private static bool _loading = false;

    public static IEnumerator LoadScene(int sceneId)
    {
        if (_loading == false)
        {
            _loading = true;

            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneId, LoadSceneMode.Single);
            asyncOperation.allowSceneActivation = false;

            {
            WaitForSceneLoad:
                yield return new WaitForSecondsRealtime(0.2f);

                if (asyncOperation.progress == 0.9f)
                {
                    _loading = false;

                    Time.timeScale = 1;

                    asyncOperation.allowSceneActivation = true;

                }

                if (asyncOperation.isDone == false)
                {
                    goto WaitForSceneLoad;
                }
            }
        }
    }

    public static void LoadSceneAdditively(int sceneId)
    {
        SceneManager.LoadSceneAsync(sceneId, LoadSceneMode.Additive);
    }
}
