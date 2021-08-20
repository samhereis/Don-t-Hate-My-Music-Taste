using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManaging : MonoBehaviour
{
    void Awake()
    {
        if (SceneManager.GetSceneByBuildIndex(1).isLoaded == false)
        {
            MenuStatics.LoadSceneAdditively(1);
        }
    }
}
