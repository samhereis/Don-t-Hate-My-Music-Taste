using Interfaces;
using System;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "GameConfigs", menuName = "Scriptables/Config/GameConfigs")]
    public class GameConfigs : ConfigBase
    {
        [field: SerializeField, Header("In game temporary")] public bool isRestart { get; set; } = false;

        [field: SerializeField] public GameSettings gameSettings = new GameSettings();
        [field: SerializeField] public RateUsConfigs rateUsSettings = new RateUsConfigs();

        [SerializeField] public GlobalReferences globalReferences;

        public override void Initialize()
        {
            rateUsSettings.Initialize();
            gameSettings.Initialize();
        }

        [Serializable]
        public class GameSettings : IInitializable
        {
            public void Initialize()
            {

            }
        }

        [Serializable]
        public class GlobalReferences
        {

        }

        [Serializable]
        public class RateUsConfigs : IInitializable
        {
            private const string _hasRated = "hasRated";
            private const string _lastClickedOnLaterButtonLevel = "lastClickedOnLaterButtonLevel";

            [field: SerializeField] public string storeLink = "";

            [field: SerializeField] public int firstAppearOnLevel { get; private set; } = 2;
            [field: SerializeField] public int showLevelIncreaseValue { get; private set; } = 5;

            public void Initialize()
            {

            }

            public bool CanShow(int currentLevel)
            {
                currentLevel++;

                if (PlayerPrefs.GetInt(_hasRated, 0) == 0)
                {
                    if (currentLevel == firstAppearOnLevel)
                    {
                        return true;
                    }

                    int lastClickedOnLaterButtonLevel = PlayerPrefs.GetInt(_lastClickedOnLaterButtonLevel, 0);
                    if (currentLevel - lastClickedOnLaterButtonLevel == showLevelIncreaseValue)
                    {
                        return true;
                    }
                }

                return false;
            }

            public void OnClickedLaterButton()
            {

            }

            public void OnRated()
            {

            }
        }
    }
}