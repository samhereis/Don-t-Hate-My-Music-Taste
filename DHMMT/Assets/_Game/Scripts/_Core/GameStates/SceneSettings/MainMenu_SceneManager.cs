using DataClasses;
using System;
using UI.Windows;
using UnityEngine;

namespace GameStates.SceneManagers
{
    public class MainMenu_SceneManager : MonoBehaviour
    {
        public Action<AScene_Extended> onLoadGameplaySceneRequest;

        [field: SerializeField, Header("Menus")] public MainMenu mainMenuReference { get; private set; }
        [field: SerializeField] public SettingsMenu settingsMenuReference { get; private set; }
        [field: SerializeField] public SceneSelectionMenu selectMapMenuReference { get; private set; }

        [SerializeField] private Camera _cameraComponent;

        public Camera cameraComponent
        {
            get
            {
                if (_cameraComponent == null) _cameraComponent = FindFirstObjectByType<Camera>();
                return _cameraComponent;
            }
        }
    }
}