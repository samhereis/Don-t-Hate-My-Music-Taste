using Sirenix.OdinInspector;
using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Services
{
    [RequireComponent(typeof(AdsManager))]
    public class AdsShowManager : MonoBehaviour
    {
        public class AdsStrings
        {
            public const string defaultInterstitial = nameof(defaultInterstitial);
            public const string defaultAppOpen = nameof(defaultAppOpen);
            public const string defaultRewarded = nameof(defaultRewarded);
            public const string defaultBanner = nameof(defaultBanner);
        }

        [Header("Comonents")]

        [Required]
        [SerializeField] private AdsManager _adsManager;

        [Header("Settings")]
        [SerializeField] private int _openAppCountToShowAppOpenAdd = 1;

#if UNITY_EDITOR
        [SerializeField] private bool _allowAppOpenInEditor = true;
#endif

        [Header("Debug")]
        [SerializeField] private int _appOpenCount = 0;

        private void Awake()
        {
            if (_adsManager == null)
            {
                _adsManager = GetComponent<AdsManager>();
            }
        }

        private async void Start()
        {
            await RequestInterstitial();
            await RequestRewarded();
            await RequestBanner();
            await RequestAppOpen();
        }

        private void OnApplicationFocus(bool focus)
        {
#if UNITY_EDITOR
            if (_allowAppOpenInEditor == false) { return; }
#endif
            if (focus == true)
            {
                bool isAppOpenCountReached = _appOpenCount >= _openAppCountToShowAppOpenAdd;

                _appOpenCount++;

                if (isAppOpenCountReached)
                {
                    TryShowAppOpen();
                }
            }
        }

        public void RemoveAds()
        {
            if (_adsManager.removedAds == false)
            {
                _adsManager.RemoveAds(true);
            }
        }

        #region Rewarded

        public async void TryShowRewarded(Action callback = null, string appID = nameof(AdsStrings.defaultRewarded))
        {
            Debug.Log("Try Show ad: Rewarded");

            bool gotRewarded = false;

            _adsManager.onPaid -= OnPaid;
            _adsManager.onRewarded -= OnRewarded;
            _adsManager.onClose -= OnAdClosed;

            _adsManager.onPaid += OnPaid;
            _adsManager.onRewarded += OnRewarded;
            _adsManager.onClose += OnAdClosed;

            if (await _adsManager.TryShowPlacement(appID) == false)
            {
                await RequestRewarded();

                await _adsManager.TryShowPlacement(appID);
            }

            void OnPaid(Placement placement, Revenue revenue)
            {
                TryGetReward();
                _adsManager.onPaid -= OnPaid;
            }

            void OnRewarded(Placement placement)
            {
                TryGetReward();
                _adsManager.onRewarded -= OnRewarded;
            }

            void OnAdClosed(Placement placement)
            {
                TryGetReward();
                _adsManager.onClose -= OnAdClosed;
            }

            void TryGetReward()
            {
                if (gotRewarded == false)
                {
                    callback?.Invoke();

                    gotRewarded = true;
                }
            }
        }

        private async Task RequestRewarded(string adID = nameof(AdsStrings.defaultRewarded))
        {
            await _adsManager.Request(adID, 5f);
        }

        #endregion

        #region Interstitial

        public async void TryShowInterstitial(string adID = nameof(AdsStrings.defaultInterstitial))
        {
            Debug.Log("Try Show ad: Interstitial");

            if (await _adsManager.TryShowPlacement(adID) == false)
            {
                await RequestInterstitial();
            }
        }

        private async Task RequestInterstitial(string adID = nameof(AdsStrings.defaultRewarded))
        {
            await _adsManager.Request(adID, 5f);
        }

        #endregion

        #region Banner

        public async void TryShowBanner(string adID = nameof(AdsStrings.defaultBanner))
        {
            Debug.Log("Try Show ad: Banner");

            if (await _adsManager.TryShowPlacement(adID) == false)
            {
                await RequestBanner();
                await _adsManager.TryShowPlacement(adID);
            }
        }

        public void DestroyBanner(string adID = nameof(AdsStrings.defaultBanner))
        {
            _adsManager.Destroy(adID);
        }

        private async Task RequestBanner(string adID = nameof(AdsStrings.defaultBanner))
        {
            await _adsManager.Request(adID, 5f);
        }

        #endregion

        #region AppOpen

        public async void TryShowAppOpen(string adID = nameof(AdsStrings.defaultAppOpen))
        {
            Debug.Log("Try Show ad: AppOpen");

            if (await _adsManager.TryShowPlacement(adID) == false)
            {
                await RequestAppOpen();
            }
            else
            {
                _appOpenCount = 0;
            }
        }

        private async Task RequestAppOpen(string adID = nameof(AdsStrings.defaultAppOpen))
        {
            await _adsManager.Request(adID, 5f);
        }

        #endregion
    }
}