// Designed by KINEMATION, 2024.

using Kinemation.FPSFramework.Runtime.Layers;
using UnityEngine;

namespace Kinemation.FPSFramework.Runtime.Core.Components
{
    public class LookAt : MonoBehaviour
    {
        [SerializeField] private Transform lookAtTarget;
        [SerializeField] private bool useLookAt;
        private LookLayer _lookLayer;

        private void Start()
        {
            _lookLayer = GetComponent<LookLayer>();
        }

        private void Update()
        {
            if (!useLookAt) return;
            
            _lookLayer.SetLookAtTarget(lookAtTarget);
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (_lookLayer == null) return;
            
            _lookLayer.SetLookAtEnabled(useLookAt);
        }
#endif
    }
}