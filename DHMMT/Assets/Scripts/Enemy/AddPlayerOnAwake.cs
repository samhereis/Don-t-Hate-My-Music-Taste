using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddPlayerOnAwake : MonoBehaviour
{
    void Awake()
    {
        StartCoroutine(AddPlayer());
    }

    IEnumerator AddPlayer()
    {
        while(true)
        {
            GetComponent<EnemyStates>().followEnemy = PlayerHealthData.instance.gameObject.transform;

            yield return Wait.NewWait(5);
        }
    }
}
