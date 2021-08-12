using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUI : MonoBehaviour, IUIPage
{
    public static ShopUI instance;

    void Awake()
    {
        instance = this;
        gameObject.SetActive(false);
    }

    void Start()
    {
    }

    void OnEnable()
    {
        
    }

    void OnDisable()
    {

    }

    public void Enable()
    {

        PlayerInput.SetInput(PlayerInput.InputState.UI);

        Cals.instance.PauseMusic(true);
        Time.timeScale = 0;

        GameplayUI.instance.Disable();

        Cursor.lockState = CursorLockMode.None;

        PlayerInput.input.UI.Back.performed += (_) => Back.OnBack();

        gameObject.SetActive(true);
    }

    public void Disable()
    {

        Cals.instance.PauseMusic(false);
        Time.timeScale = 1;

        GameplayUI.instance.Enable();

        Cursor.lockState = CursorLockMode.Locked;


        PlayerInput.SetInput(PlayerInput.InputState.Gameplay);

        PlayerInput.input.UI.Back.performed -= (_) => Back.OnBack();

        gameObject.SetActive(false);
    }
}
