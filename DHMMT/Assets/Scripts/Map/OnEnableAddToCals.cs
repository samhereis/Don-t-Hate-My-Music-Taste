using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEnableAddToCals : MonoBehaviour
{
    // When this object enables add this object to "MakeObjectsShake" list;

    [SerializeField] private int _indexInReactors;

    private void Awake()
    {
        _indexInReactors = Random.Range(0, 5);
    }

    private void OnEnable()
    {
        if(MakeObjectsShake.instance.enabled == false)
        {
            MakeObjectsShake.instance.enabled = true;
        }

        MakeObjectsShake.instance.ObjectsReactingToBasses.Add(transform);
    }

    private void OnDisable()
    {
        MakeObjectsShake.instance.ObjectsReactingToBasses.Add(transform);
    }
}
