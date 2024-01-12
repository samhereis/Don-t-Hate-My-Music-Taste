using TMPro;
using UnityEngine;

namespace UI.Elements.GameplayTab
{
    public class KillsCountDisplayer : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private TextMeshProUGUI _killsCountText;

        [Header("Debug")]
        [SerializeField] private int _killsCount;

        private void Awake()
        {
            ResetKillsCount();
        }

        public void SetKillsCount(int killsCount)
        {
            _killsCount = killsCount;
            UpdateKillsCount();
        }

        public void IncreaseKillsCount(int increaseValue = 1)
        {
            _killsCount += increaseValue;
            UpdateKillsCount();
        }

        public void DecreaseKillsCount(int decreaseValue = 1)
        {
            _killsCount -= decreaseValue;
            UpdateKillsCount();
        }

        public void ResetKillsCount()
        {
            _killsCount = 0;
            UpdateKillsCount();
        }

        private void UpdateKillsCount()
        {
            _killsCountText.text = _killsCount.ToString();
        }
    }
}