using TMPro;
using UnityEngine;

namespace UI
{
    public class MessageToPlayer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;

        private void OnEnable()
        {
            _text = GetComponent<TextMeshProUGUI>();
        }

        public void ShowMessage(string message)
        {
            _text.text = message;
        }
    }
}