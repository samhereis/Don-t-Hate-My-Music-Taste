using DataClasses;
using DI;
using System;
using UI.Windows;
using UnityEngine;

namespace GameStates.SceneManagers
{
    public class MainMenu_SceneManager : Scene_SceneManagerBase, IDIDependent
    {
        public Action<AScene_Extended> onLoadGameplaySceneRequest;

        [field: SerializeField, Header("Menus")] public MainMenu mainMenuReference { get; private set; }
        [field: SerializeField] public SettingsMenu settingsMenuReference { get; private set; }
        [field: SerializeField] public SceneSelectionMenu selectMapMenuReference { get; private set; }
    }
}