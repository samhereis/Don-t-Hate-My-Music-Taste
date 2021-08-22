using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnIfPlayerIsNear : MonoBehaviour
{
    [SerializeField] List<GameObject> objects;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerGunUse>())
        {
            foreach (var item in objects)
            {
                item.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerGunUse>())
        {
            foreach (var item in objects)
            {
                item.SetActive(false);
            }
        }
    }
}
