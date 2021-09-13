using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchForInteractable : MonoBehaviour
{
    // Constantly cast raycast to see near interactable objects

    public static SearchForInteractable instance;

    public IInteractable Interactable;

    private void OnEnable()
    {
        instance = this;

        PlayerInput.PlayersInputState.Gameplay.Interact.performed += _ => { Interact(); };
    }

    private void FixedUpdate()
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
