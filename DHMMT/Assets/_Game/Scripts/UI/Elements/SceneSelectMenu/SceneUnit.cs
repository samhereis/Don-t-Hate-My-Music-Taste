using Configs;
using ConstStrings;
using DataClasses;
using DependencyInjection;
using DG.Tweening;
using Helpers;
using Interfaces;
using Observables;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Elements.SceneSelectMenu
{
    public class SceneUnit : MonoBehaviour, IInitializable<AScene_Extended>, INeedDependencyInjection, IPointerEnterHandler, IPointerExitHandler
    {
        [Header("Components")]
        [SerializeField] private Image _sceneImage;
        [SerializeField] private TextMeshProUGUI _sceneNameText;
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _selectButton;

        [Space(10)]
        [SerializeField] private CanvasGroup _onSelectPanel;

        [Header("DI")]
        [Inject] private UIConfigs _uiConfigs;
        [Inject(DataSignal_ConstStrings.onASceneSelected)] private DataSignal<AScene_Extended> _onASceneSelected;
        [Inject(DataSignal_ConstStrings.onASceneLoadRequested)] private DataSignal<AScene_Extended> _onASceneLoadRequested;

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
            DependencyContext.diBox.InjectDataTo(this);

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

            await AsyncHelper.DelayFloat(_playButtonDelay);
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