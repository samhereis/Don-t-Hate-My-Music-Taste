using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SecondGunSlotUI : MonoBehaviour
    {
        [SerializeField] private Color _activeColor;
        [SerializeField] private Color _inactiveColor;
        [SerializeField] private Image _image;

        public void Activate()
        {
            _image.color = _activeColor;
        }

        public void Deactivate()
        {
            _image.color = _inactiveColor;
        }
    }
}