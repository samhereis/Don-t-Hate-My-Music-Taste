using Identifiers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableShop : MonoBehaviour, IInteractable
{
    // How shop interacts with an interactor

    public bool isInteractable { get => true; set => throw new NotImplementedException(); }

    public string ItemName { get => "Open Shop"; set => ItemName = value; }
    public GameObject InteractableObject { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public void Interact(IdentifierBase caller)
    {
        ShopUI.instance.Enable();
    }
}
