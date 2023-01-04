using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class FirstGunSlotUI : MonoBehaviour
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