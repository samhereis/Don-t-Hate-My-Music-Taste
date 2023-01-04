using Helpers;
using IdentityCards;
using TMPro;
using UI.Canvases;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Values;

namespace UI
{
    public sealed class SceneSelectButton : MonoBehaviour, IPointerClickHandler
    {
        [Header("SO")]
        [SerializeField] private CurrentScene_SO _currentScene;

        [Header("Components")]
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private Image _icon;

        [Header("Settings")]
        [SerializeField] private string _sceneCode;
        [SerializeField] private bool _isActive = true;

        public void SetScene(ASceneIdentityCard aScene)
        {
            _sceneCode = aScene.target;
            _text.text = aScene.targetName;
            _icon.sprite = aScene.icon;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_isActive == false) return;
            if (_currentScene.value == _sceneCode) return;

            _currentScene.ChangeValue(_sceneCode);
            MessageToUser.instance.Log("Map selected");
        }

        [ContextMenu(nameof(Setup))]
        public async void Setup()
        {
            if (_currentScene == null) _currentScene = await AddressablesHelper.GetAssetAsync<CurrentScene_SO>("CurrentScene");
        }
    }
}