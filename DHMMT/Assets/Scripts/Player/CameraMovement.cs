using UnityEngine.InputSystem;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public static CameraMovement instance;
    [SerializeField] [Range(0, 1)] public float mouseSensitivity = 0.2f;
    Vector2 context;
    public Transform moveCameraTowards;
    public Transform playerBody;
    float mouseX; float mouseY;
    float XRotation;
    void Awake()
    {
        transform.position = moveCameraTowards.position;

        instance = this;

        Cursor.lockState = CursorLockMode.Locked;
    }
    void OnEnable()
    {
        PlayerInput.input.Gameplay.Look.performed += _ => { Look(_); };
        PlayerInput.input.Gameplay.Look.canceled += _ => { Look(_); };
    }
    void Update()
    {
        transform.position = moveCameraTowards.position;

        XRotation -= mouseY;
        XRotation = Mathf.Clamp(XRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(XRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
    void Look(InputAction.CallbackContext ctx)
    {
        context = ctx.ReadValue<Vector2>() * mouseSensitivity;

        mouseX = context.x;
        mouseY = context.y;
    }
}
