using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkPlayerInRange : MonoBehaviour
{
    PlayerHealthData player;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerHealthData>())
        {
            player = other.GetComponent<PlayerHealthData>();
            if(player.numberOfCheckers.Contains(this) == false)
            {
                player.numberOfCheckers.Add(this);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerHealthData>().numberOfCheckers.Count > 0)
        {
            if (player.numberOfCheckers.Contains(this) == true)
            {
                player.numberOfCheckers.Remove(this);
            }
        }
    }
}
