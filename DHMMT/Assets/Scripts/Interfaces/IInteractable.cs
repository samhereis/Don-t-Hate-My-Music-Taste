using System;
using UnityEngine;

public interface IInteractable
{
    // Blueprint of an interactable  object

    bool Interactable { get; set; }
    string ItemName { get; set; }
    GameObject InteractableObject { get; set; }
    void Interact(GameObject caller);
}
