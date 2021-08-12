using System;
using UnityEngine;

public interface IInteractable
{
    string ItemName { get; set; }
    GameObject InteractableObject { get; set; }
    void Interact(GameObject caller);
}
