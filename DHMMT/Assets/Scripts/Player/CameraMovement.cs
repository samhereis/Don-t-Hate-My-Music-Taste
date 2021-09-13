using UnityEngine.InputSystem;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // Controlls main players camera

    public static CameraMovement instance;

    [SerializeField] [Range(0, 1)] public float MouseSensitivity = 0.2f;

    private Vector2 _context;

    public Transform MoveCameraTowards;

    public Transform PlayerBody;

    private float _mouseX;
    private float _mouseY;

    private float _xRotation;

    private void Awake()
    {
        transform.position = MoveCameraTowards.position;

        if(GameSettings.instance == null)
        {
            PauseUI.instance.Enable();
            PauseUI.instance.Disable();
        }

        instance = this;

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnEnable()
    {
        PlayerInput.PlayersInputState.Gameplay.Look.performed += Look;
        PlayerInput.PlayersInputState.Gameplay.Look.canceled += Look;
    }

    private void OnDisable()
    {
        PlayerInput.PlayersInputState.Gameplay.Look.performed -= Look;
        PlayerInput.PlayersInputState.Gameplay.Look.canceled -= Look;
    }

    private void Update()
    {
        transform.position = MoveCameraTowards.position;

        _xRotation -= _mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
        PlayerBody.Rotate(Vector3.up * _mouseX);
    }

    private void Look(InputAction.CallbackContext ctx)
    {
        _context = ctx.ReadValue<Vector2>() * GameSettings.instance.SensitivityValue;

        _mouseX = _context.x;
        _mouseY = _context.y;
    }

    public void Shake()
    {
        transform.position = MoveCameraTowards.position;

        AnimationStatics.NormalShake(transform, 2);
    }
}
