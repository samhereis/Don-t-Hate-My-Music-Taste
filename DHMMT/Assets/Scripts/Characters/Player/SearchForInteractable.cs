using Scriptables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchForInteractable : MonoBehaviour
{
    // Constantly cast raycast to see near interactable objects

    public static SearchForInteractable instance;

    public IInteractable Interactable;

    [SerializeField] private Input_SO _inputContainer;
    private InputSettings _input => _inputContainer.input;

    private void OnEnable()
    {
        instance = this;

        _input.Gameplay.Interact.performed += _ => { Interact(); };
    }

    private void FixedUpdate()
    {

    }

    public void Interact()
    {
        Interactable?.Interact(gameObject);
    }
}
