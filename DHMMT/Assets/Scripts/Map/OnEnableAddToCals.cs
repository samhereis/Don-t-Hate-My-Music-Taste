using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEnableAddToCals : MonoBehaviour
{
    int indexInReactors;

    private void Awake()
    {
        indexInReactors = Random.Range(0, 5);
    }

    void OnEnable()
    {
        if(MakeObjectsShake.instance.enabled == false)
        {
            MakeObjectsShake.instance.enabled = true;
        }

        MakeObjectsShake.instance.objectsReactingToBasses.Add(transform);
    }

    void OnDisable()
    {
        MakeObjectsShake.instance.objectsReactingToBasses.Add(transform);
    }
}
