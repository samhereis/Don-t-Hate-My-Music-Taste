using Identifiers;
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

    [SerializeField] private IdentifierBase _identifier;

    private void OnValidate()
    {
        if (_identifier == null) _identifier = GetComponent<IdentifierBase>();
    }

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
        _interactable?.Interact(_identifier);
    }
}
