using System;
using UnityEngine;

namespace DataClasses
{
    [Serializable]
    public class AScene_Extended : AScene
    {
        [field: SerializeField, Header("Settings")] public string sceneName { get; private set; }
        [field: SerializeField, TextArea] public string description { get; private set; }
        [field: SerializeField, TextArea] public string rules { get; private set; }
        [field: SerializeField] public Sprite icon { get; private set; }

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
    }
}