using Scriptables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SearchForInteractable : MonoBehaviour
{
    // Constantly cast raycast to see near interactable objects
    private IInteractable _interactable;

    [SerializeField] private Input_SO _inputContainer;
    private InputSettings _input => _inputContainer.input;

    private void OnEnable()
    {
        _input.Gameplay.Interact.performed += Interact;
    }

    private void OnDisable()
    {
        _input.Gameplay.Interact.performed -= Interact;
    }

    private void Interact(InputAction.CallbackContext context)
    {
        _interactable?.Interact(gameObject);
    }
}
