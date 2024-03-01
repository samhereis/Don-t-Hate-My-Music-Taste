using DataClasses;
using DependencyInjection;
using Helpers;
using Identifiers;
using Interfaces;
using Music;
using SO.Lists;
using UnityEngine;

namespace GameStates
{
    public class MainMenu_Scene : MonoBehaviour, IInitializable, INeedDependencyInjection
    {
        [SerializeField] private Camera _cameraComponent;
        [SerializeField] private MusicInitializer _musicInitializer;
        [SerializeField] private PlayingMusicData _playingMusicData;

        [Inject] private ListOfAllScenes_Extended _listOfAllScenes;

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
            DependencyContext.InjectDependencies(this);

            SetupBackgroundScene();

            _musicInitializer?.Initialize();
            _playingMusicData?.Initialize();
        }

        private void SetupBackgroundScene()
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
    }
}