using IdentityCards;
using UnityEngine;

namespace Values
{
    [CreateAssetMenu(fileName = "CurrentScene", menuName = "Scriptables/Values/Concrete/CurrentScene")]
    public sealed class CurrentScene_SO : StringValue_SO
    {
        private string currentScene => PlayerPrefs.GetString("CurrentScene", "PeakyIsland");

        public override string value { get => value = currentScene; protected set => base.value = value; }
        public void Validate()
        {
            if (string.IsNullOrEmpty(value)) value = currentScene;
        }

        public override void ChangeValue(string sentValue)
        {
            PlayerPrefs.SetString("CurrentScene", sentValue);
            base.ChangeValue(sentValue);
        }

        public void ChangeValue(ASceneIdentityCard aScene)
        {
            PlayerPrefs.SetString("CurrentScene", aScene.target);
            base.ChangeValue(aScene.target);
        }
    }
}