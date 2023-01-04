using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "Match", menuName = "Scriptable Object/Match")]
    public class MatchSO : ScriptableObject //TODO: rewrite this, this is a too old script
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
            PlayerPrefs.Save();
        }
    }
}