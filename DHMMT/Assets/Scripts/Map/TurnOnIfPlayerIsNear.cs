using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnIfPlayerIsNear : MonoBehaviour
{
    // If player is near, turn every objects in "this.objects" on

    [SerializeField] private List<GameObject> _objects;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerGunUse>())
        {
            foreach (var item in _objects)
            {
                item.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerGunUse>())
        {
            foreach (var item in _objects)
            {
                item.SetActive(false);
            }
        }
    }
}
