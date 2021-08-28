using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TS_D_OnEnemyDie : MonoBehaviour
{
    [SerializeField] int SecondsAmmountToAddWhenDying = 10;

    private void OnDisable()
    {
        SecondsCount.instance.ChagneSeconds(SecondsCount.instance._seconds + SecondsAmmountToAddWhenDying);
    }
}
