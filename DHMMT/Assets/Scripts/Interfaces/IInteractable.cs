using Identifiers;
using System;
using UnityEngine;

public interface IInteractable
{
    bool isInteractable { get; }
    string ItemName { get; }
    void Interact(IdentifierBase caller);
}
