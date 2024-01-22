using Helpers;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace UI.Windows
{
    public class LoadingMenu : SimpleLoadingMenu
    {
        [Header("UI Elements")]
        [SerializeField] private Transform _loadingCircle;
        [SerializeField] private VideoPlayer _videoPlayer;

        [Header("Settings")]
        [SerializeField] private float _loadingCirlceRotationSpeed = 30;

        [SerializeField] private List<VideoClip> _videoClips = new List<VideoClip>();

        protected override void Awake()
        {
            base.Awake();

            if (_videoPlayer == null) { _videoPlayer = GetComponentInChildren<VideoPlayer>(); }

            if (_videoClips.Count > 0)
            {
                _videoPlayer.clip = _videoClips.GetRandom();
            }
        }

        private void Update()
        {
            if (_loadingCircle != null)
            {
                _loadingCircle.eulerAngles = new Vector3(0, 0, _loadingCircle.eulerAngles.z - _loadingCirlceRotationSpeed * Time.deltaTime);
            }
        }
    }
}