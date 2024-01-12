using UnityEngine;

namespace Identifiers
{
    [RequireComponent(typeof(Camera))]
    public class MainCamera_Identifier : IdentifierBase
    {
        [field: SerializeField] public Camera cameraComponent { get; private set; }

        private void Awake()
        {
            cameraComponent = TryGet<Camera>();
        }
    }
}