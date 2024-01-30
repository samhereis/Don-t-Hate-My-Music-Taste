using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace DataClasses
{
    [Serializable]
    public class AScene_Extended : AScene
    {
        public enum GameMode { EscapeFromHaters, TsukuyomiDream }

        [field: SerializeField, FoldoutGroup("Settings")] public string sceneName { get; private set; }
        [field: SerializeField, FoldoutGroup("Settings")] public GameMode gameMode { get; private set; }
        [field: SerializeField, FoldoutGroup("Settings")] public Sprite icon { get; private set; }
        [field: SerializeField, FoldoutGroup("Descriptions"), TextArea] public string description { get; private set; }
        [field: SerializeField, FoldoutGroup("Descriptions"), TextArea] public string rules { get; private set; }
        [field: SerializeField] public BackgroundSceneSettings backgroundSceneSettings { get; private set; } = new BackgroundSceneSettings();

        public Sprite GetIcon()
        {
            return icon;
        }

        public string GetSceneName()
        {
            return sceneName;
        }

        public string GetDescription()
        {
            return description;
        }

        public string GetRules()
        {
            return rules;
        }

        [Serializable]
        public class BackgroundSceneSettings
        {
            [field: SerializeField] public GameObject visuals { get; private set; }
            [field: SerializeField] public Material[] skyboxes { get; private set; }
            [field: SerializeField] public float ambientIntencity { get; private set; } = 1f;
        }
    }
}