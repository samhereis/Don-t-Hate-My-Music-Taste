using UnityEngine;

namespace Identifiers
{
    public class CameraPositionIdentifier_Identifier : IdentifierBase
    {
        public Transform lightTargetTransform
        {
            get
            {
                if (_lightPosition == null) { _lightPosition = transform; }
                return _lightPosition;
            }
        }

        [SerializeField] private Transform _lightPosition;
    }
}