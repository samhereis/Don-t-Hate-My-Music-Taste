using Helpers;
using Mirror;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NetworkIdentityHelper : NetworkBehaviour
{
    [SerializeField] private List<Behaviour> _componentsToHandle = new List<Behaviour>();

    private void Start()
    {
        try
        {
            if (isLocalPlayer == false)
            {
                foreach (var component in _componentsToHandle)
                {
                    component.enabled = false;
                }
            }
        }
        finally
        {

        }
    }

    [ContextMenu(nameof(Setup))]
    public void Setup()
    {
        _componentsToHandle = GetComponentsInChildren<Behaviour>().ToList();
        this.TrySetDirty();
    }
}