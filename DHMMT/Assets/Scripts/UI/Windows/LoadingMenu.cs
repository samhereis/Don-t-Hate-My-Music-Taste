using Helpers;
using System.Collections.Generic;
using UI.Canvases;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace UI.Windows
{
    public class LoadingMenu : CanvasWindowBase
    {
        [Header("UI Elements")]
        [SerializeField] private Transform _loadingCircle;
        [SerializeField] private Slider _progressSlider;
        [SerializeField] private VideoPlayer _videoPlayer;

        [Header("Settings")]
        [SerializeField] private float _loadingCirlceRotationSpeed = 30;

        [SerializeField] private List<AssetReferenceVideoClip> _videoClipAddressables = new List<AssetReferenceVideoClip>();

        protected override async void Awake()
        {
            base.Awake();

            if (_videoPlayer == null) { _videoPlayer = GetComponentInChildren<VideoPlayer>(); }

            if (_videoClipAddressables.Count > 0)
            {
                var videoClip = await AddressablesHelper.GetAssetAsync<VideoClip>(_videoClipAddressables.GetRandom());
                _videoPlayer.clip = videoClip;
            }
        }

        private void Update()
        {
            if (_loadingCircle != null)
            {
                _loadingCircle.eulerAngles = new Vector3(0, 0, _loadingCircle.eulerAngles.z - _loadingCirlceRotationSpeed * Time.deltaTime);
            }
        }

        public void SetProgress(float progress)
        {
            _progressSlider.value = progress;
        }
    }
}