using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchForInteractable : MonoBehaviour
{
    public static SearchForInteractable instance;
    public IInteractable Interactable;

    void OnEnable()
    {
        instance = this;

        PlayerInput.input.Gameplay.Interact.performed += _ => { Interact(); };
    }

    void FixedUpdate()
    {
        if(CameraRaycast.Cast(out Interactable) && Interactable.Interactable == true)
        {
            MessageScript.instance.InteractMessage.SetActive(true);
        }
        else
        {
            MessageScript.instance.InteractMessage.SetActive(false);
        }
    }

    public void Interact()
    {
        Interactable?.Interact(gameObject);
    }
}
