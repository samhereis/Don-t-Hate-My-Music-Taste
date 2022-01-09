using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    // Scene loader attachable on gameobject

    public void LoadScene(int index)
    {
        LoadingWindow.instance?.Open();

        StartCoroutine(SceneLoadController.LoadScene(index));
    }
}
