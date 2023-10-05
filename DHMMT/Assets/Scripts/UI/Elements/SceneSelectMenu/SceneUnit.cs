using Configs;
using ConstStrings;
using DataClasses;
using DG.Tweening;
using DI;
using Events;
using Helpers;
using Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Elements.SceneSelectMenu
{
    public class SceneUnit : MonoBehaviour, IInitializable<AScene_Extended>, IDIDependent, IPointerEnterHandler, IPointerExitHandler
    {
        [Header("Components")]
        [SerializeField] private Image _sceneImage;
        [SerializeField] private TextMeshProUGUI _sceneNameText;
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _selectButton;

        [Space(10)]
        [SerializeField] private CanvasGroup _onSelectPanel;

        [Header("DI")]
        [DI(DIStrings.uiConfigs)][SerializeField] private UIConfigs _uiConfigs;
        [DI(Event_DIStrings.onASceneSelected)][SerializeField] private EventWithOneParameters<AScene_Extended> _onASceneSelected;
        [DI(Event_DIStrings.onASceneLoadRequested)][SerializeField] private EventWithOneParameters<AScene_Extended> _onASceneLoadRequested;

        [Header("Debug")]
        [SerializeField] private AScene_Extended _scene;

        private float _scaleDuration => _uiConfigs != null ? _uiConfigs.uiScaleAnimationDuration : UIConfigs.defaultUIScaleAnimationDuration;
        private Ease _scaleEase => _uiConfigs != null ? _uiConfigs.uiScaleEase : UIConfigs.defaultUIScaleEase;
        private float _playButtonDelay => _uiConfigs != null ? _uiConfigs.scemeSelectMenuConfigs.onPlayRightAwayDelay : 1;

        private void Awake()
        {
            _playButton?.onClick.AddListener(OnPlayClicked);
            _selectButton?.onClick.AddListener(OnSelectClicked);
        }

        public void Initialize(AScene_Extended type)
        {
            (this as IDIDependent)?.LoadDependencies();

            _scene = type;

            _sceneImage.sprite = _scene.GetIcon();
            _sceneNameText.text = _scene.GetSceneName();

            _onSelectPanel.FadeDownQuick(setActiveToFalse: true);

            transform.localScale = Vector3.zero;
            transform.DOScale(1, _scaleDuration).SetEase(_scaleEase);
        }

        private void OnDestroy()
        {
            _onSelectPanel.DOKill();
            transform.DOKill();
        }

        private async void OnPlayClicked()
        {
            OnSelectClicked();

            await AsyncHelper.Delay(_playButtonDelay);
            _onASceneLoadRequested?.Invoke(_scene);
        }

        private void OnSelectClicked()
        {
            _onASceneSelected?.Invoke(_scene);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _onSelectPanel.FadeUp(0.5f);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _onSelectPanel.FadeDown(0.5f, setActiveToFalse: true);
        }
    }
}