using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCamera : MonoBehaviour
{
    public Transform moveCameraTowards;

    void Awake()
    {
        transform.position = moveCameraTowards.position;
    }
}
