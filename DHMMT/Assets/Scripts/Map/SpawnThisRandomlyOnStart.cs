using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnThisRandomlyOnStart : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        StartCoroutine:

        yield return new WaitForSecondsRealtime(2);

        if (SpawnPoints.instance._spawnPointsList.Count < 1)
        {
            goto StartCoroutine;
        }
        else
        {
            transform.parent = SpawnPoints.instance.GetRandomSpawn();

            transform.localPosition = new Vector3(0, -2, 0);
        }

        StopAllCoroutines();
    }
}
