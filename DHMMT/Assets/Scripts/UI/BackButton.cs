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

    private void Back(InputAction.CallbackContext context)
    {
        PlayerInput.PlayersInputState.UI.Back.performed -= Back;
        instance = null;

        if (ButtonComponent != null)
        {
            ButtonComponent.onClick.Invoke();
        }
    }
}
