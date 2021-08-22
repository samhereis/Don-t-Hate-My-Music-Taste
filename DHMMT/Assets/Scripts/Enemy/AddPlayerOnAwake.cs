using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddPlayerOnAwake : MonoBehaviour
{
    private void OnEnable()
    {
        GetComponent<EnemyStates>().followEnemy = CameraRaycast.instance.transform;
    }
}
