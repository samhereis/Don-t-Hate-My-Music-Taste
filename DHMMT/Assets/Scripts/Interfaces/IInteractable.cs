using System;
using UnityEngine;

public interface IInteractable
{
    bool Interactable { get; set; }
    string ItemName { get; set; }
    GameObject InteractableObject { get; set; }
    void Interact(GameObject caller);
}
