using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Match", menuName = "Scriptable Object/Match")]
public class MatchSO : ScriptableObject
{
    public string SceneCodeName;
    public int SceneId;

    public string RecordCode { get { return $"{SceneId}_{SceneCodeName}_Record"; } }

    public int GetRecordForTheScene()
    {
        return PlayerPrefs.GetInt(RecordCode);
    }

    public void SetRecordForTheScene(int value)
    {
        PlayerPrefs.SetInt(RecordCode, value);
    }
}
