using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class MenuStatics
{
    public static Transform LoadingWindow;
    static bool Loading = false;

    public static IEnumerator LoadScene(int sceneId)
    {
        if (Loading == false)
        {
            Loading = true;

            if (LoadingWindow != null)
            {
                LoadingWindow.gameObject.SetActive(true);
            }

            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneId, LoadSceneMode.Single);
            asyncOperation.allowSceneActivation = false;

            {
            WaitForSceneLoad:
                yield return new WaitForSecondsRealtime(0.2f);

                if (asyncOperation.progress == 0.9f)
                {
                    Loading = false;
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
    public static IEnumerator LoadScene(int sceneId, int scene2Id)
    {
        if (Loading == false)
        {
            Loading = true;

            if (LoadingWindow != null)
            {
                LoadingWindow.gameObject.SetActive(true);
            }

            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneId, LoadSceneMode.Single);
            LoadSceneAdditively(scene2Id);
            asyncOperation.allowSceneActivation = false;

            {
            WaitForSceneLoad:
                yield return new WaitForSecondsRealtime(0.2f);

                if (asyncOperation.progress == 0.9f)
                {
                    Loading = false;
                    Time.timeScale = 1;
                    asyncOperation.allowSceneActivation = true;
                }

                if (asyncOperation.isDone == false)
                {
                    Debug.Log(asyncOperation.progress);
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
