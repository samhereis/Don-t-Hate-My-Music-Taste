using UnityEngine;

namespace Gameplay.Camera
{
    public class UICamera : MonoBehaviour
    {
        [SerializeField] private UnityEngine.Camera _uICamera;

        private void Awake()
        {
            if (_uICamera == null) _uICamera = GetComponentInChildren<UnityEngine.Camera>();
        }

        public void SetEnabled(bool value)
        {

        }
    }
}