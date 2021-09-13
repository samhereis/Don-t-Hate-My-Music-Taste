using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManaging : MonoBehaviour
{
    // Usually used to load UI

    private void Awake()
    {
        if (SceneManager.GetSceneByBuildIndex(1).isLoaded == false)
        {
            SceneLoadController.LoadSceneAdditively(1);
        }
    }
}
