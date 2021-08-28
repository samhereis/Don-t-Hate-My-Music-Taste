using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordUpdater : MonoBehaviour
{
    public static RecordUpdater instance;

    public string NameOfScene;

    private void Awake()
    {
        ExtentionMethods.SetWithNullCheck(ref instance, this);
    }

    public void Save(int value)
    {
        PlayerPrefs.SetFloat(NameOfScene, value);
        PlayerPrefs.Save();
    }
}
