using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkPlayerInRange : MonoBehaviour
{
    // Check if main player is in range of this object's Collider

    private PlayerHealthData _player;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerHealthData>())
        {
            _player = other.GetComponent<PlayerHealthData>();
            if(_player.numberOfCheckers.Contains(this) == false)
            {
                _player.numberOfCheckers.Add(this);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other?.GetComponent<PlayerHealthData>()?.numberOfCheckers.Count > 0)
        {
            if (_player.numberOfCheckers.Contains(this) == true)
            {
                _player.numberOfCheckers.Remove(this);
            }
        }
    }
}
