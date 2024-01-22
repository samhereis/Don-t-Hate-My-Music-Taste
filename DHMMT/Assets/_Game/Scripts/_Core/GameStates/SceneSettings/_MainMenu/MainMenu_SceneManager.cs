using DataClasses;
using DependencyInjection;
using Helpers;
using Identifiers;
using Interfaces;
using SO.Lists;
using System;
using UI.Windows;
using UnityEngine;

namespace GameStates
{
    public class MainMenu_SceneManager : MonoBehaviour, IInitializable
    {
        public Action<AScene_Extended> onLoadGameplaySceneRequest;

        [field: SerializeField] public MainMenu mainMenuReference { get; private set; }
        [field: SerializeField] public SettingsMenu settingsMenuReference { get; private set; }
        [field: SerializeField] public SceneSelectionMenu selectMapMenuReference { get; private set; }

        [SerializeField] private Camera _cameraComponent;

        [Inject] private ListOfAllScenes _listOfAllScenes;

        private AScene_Extended _currentBackgroundScene;
        private GameObject _currentBackgroundSceneVisuals;

        public Camera cameraComponent
        {
            get
            {
                if (_cameraComponent == null) _cameraComponent = FindFirstObjectByType<Camera>();
                return _cameraComponent;
            }
        }

        public void Initialize()
        {
            SetupBackgroundScene();
        }

        private void SetupBackgroundScene()
        {
            try
            {
                _currentBackgroundScene = _listOfAllScenes.GetScenes().GetRandom();
                if (_currentBackgroundScene == null || _currentBackgroundScene.backgroundSceneSettings.visuals == null) return;

                _currentBackgroundSceneVisuals = Instantiate(_currentBackgroundScene.backgroundSceneSettings.visuals, Vector3.zero, Quaternion.identity);
                RenderSettings.skybox = _currentBackgroundScene.backgroundSceneSettings.skyboxes.GetRandom();
                RenderSettings.ambientIntensity = _currentBackgroundScene.backgroundSceneSettings.ambientIntencity;

                var cameraPosition = FindFirstObjectByType<CameraPositionIdentifier_Identifier>(FindObjectsInactive.Include);
                cameraComponent.transform.SetParent(cameraPosition.transform, false);
                cameraComponent.transform.localPosition = Vector3.zero;

                var lightForMainMenu = FindFirstObjectByType<LightForMainMenudentifier>(FindObjectsInactive.Include);
                if (lightForMainMenu != null)
                {
                    lightForMainMenu.transform.rotation = cameraPosition.lightTargetTransform.rotation;
                    lightForMainMenu.transform.position = cameraPosition.lightTargetTransform.position;
                }
            }
            catch
            {

            }
        }
    }
}