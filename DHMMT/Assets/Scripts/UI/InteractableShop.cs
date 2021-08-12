using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableShop : MonoBehaviour, IInteractable
{
    public string ItemName { get => "Open Shop"; set => ItemName = value; }
    public GameObject InteractableObject { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public void Interact(GameObject caller)
    {
        ShopUI.instance.Enable();
    }
}
