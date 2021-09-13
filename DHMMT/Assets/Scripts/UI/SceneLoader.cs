using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    // Scene loader attachable on gameobject

    public void LoadScene(int index)
    {
        if(LoadingWindow.instance != null)
        {
            LoadingWindow.instance.Window.SetActive(true);

            StartCoroutine(SceneLoadController.LoadScene(index, LoadingWindow.instance.Window));
        }
        else
        {
            StartCoroutine(SceneLoadController.LoadScene(index));
        }
    }
}
