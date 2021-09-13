using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class BackButton : MonoBehaviour
{
    // UI's back button

    public static BackButton instance;

    public Button ButtonComponent;

    private void OnEnable()
    {
        instance = this;

        PlayerInput.PlayersInputState.UI.Back.performed += Back;
    }

    private void OnDisable()
    {
        instance = null;
        PlayerInput.PlayersInputState.UI.Back.performed -= Back;
    }

    private void Back(InputAction.CallbackContext context)
    {
        if (ButtonComponent != null)
        {
            ButtonComponent.onClick.Invoke();
        }
    }
}
