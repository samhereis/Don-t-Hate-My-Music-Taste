using System;
using UnityEngine;

public interface IInteractable
{
    // Blueprint of an interactable  object

    bool isInteractable { get; }
    string ItemName { get; }
    void Interact(GameObject caller);
}
