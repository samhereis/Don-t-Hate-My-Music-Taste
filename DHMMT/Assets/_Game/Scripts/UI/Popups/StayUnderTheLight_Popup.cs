using TMPro;
using UnityEngine;

namespace UI.Popups
{
    public class StayUnderTheLight_Popup : PopupBase_Scale
    {
        [Header("Components")]
        [SerializeField] private TextMeshProUGUI _secondsText;

        public void SetSeconds(int seconds)
        {
            _secondsText.text = seconds.ToString();
        }
    }
}