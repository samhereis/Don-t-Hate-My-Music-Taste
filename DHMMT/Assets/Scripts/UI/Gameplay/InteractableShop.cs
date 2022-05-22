using Identifiers;
using Interfaces;
using System;
using UnityEngine;

namespace UI
{
    public class InteractableShop : MonoBehaviour, IInteractable
    {
        public bool isInteractable { get => true; set => throw new NotImplementedException(); }
        public string ItemName { get => "Open Shop"; set => ItemName = value; }

        public void Interact(IdentifierBase caller)
        {
            //ShopUI.instance.Enable();
        }
    }
}