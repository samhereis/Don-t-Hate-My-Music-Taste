using UnityEngine;

namespace DataClasses
{
    public class FPSData : MonoBehaviour
    {
        [field: SerializeField] public bool isChangeWeapon { get; set; }
        [field: SerializeField] public bool isReload { get; set; }
        [field: SerializeField] public bool isThwowGranade { get; set; }
        [field: SerializeField] public bool isRightLean { get; set; }
        [field: SerializeField] public bool leftLean { get; set; }
        [field: SerializeField] public float mouseScrollWeel { get; set; }
        [field: SerializeField] public bool isFirePressed { get; set; }
        [field: SerializeField] public bool isFireReleased { get; set; }
        [field: SerializeField] public bool isToggleAim { get; set; }
        [field: SerializeField] public bool isChangeScope { get; set; }
        [field: SerializeField] public bool isB { get; set; }
        [field: SerializeField] public bool isH { get; set; }
        [field: SerializeField] public bool isFreeLook { get; set; }
        [field: SerializeField] public float deltaMouseX { get; set; }
        [field: SerializeField] public float deltaMouseY { get; set; }
        [field: SerializeField] public bool isJump { get; set; }
        [field: SerializeField] public bool isSprint { get; set; }
        [field: SerializeField] public bool isSlide { get; set; }
        [field: SerializeField] public bool isProne { get; set; }
        [field: SerializeField] public bool isCrouch { get; set; }
        [field: SerializeField] public float moveXRaw { get; set; }
        [field: SerializeField] public float moveYRaw { get; set; }
    }
}