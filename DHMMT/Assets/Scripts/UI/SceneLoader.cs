using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    public void LoadScene(int index)
    {
        LoadingWindow.instance.Window.SetActive(true);

        StartCoroutine(MenuStatics.LoadScene(index, LoadingWindow.instance.Window));
    }
}
