using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchForInteractable : MonoBehaviour
{
    public static SearchForInteractable instance;
    public IInteractable Interactable;
    public string T;

    void OnEnable()
    {
        instance = this;

        PlayerInput.input.Gameplay.Interact.performed += _ => { Interact(); };
    }

    void FixedUpdate()
    {
        if(CameraRaycast.Cast(out Interactable))
        {
            T = Interactable.ItemName;
        }
        else
        {
            Interactable = null;
            T = null;
        }
    }

    public void Interact()
    {
        Interactable?.Interact(gameObject);
    }
}
