using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class W_L_N_OnEnemyDie : MonoBehaviour, IOnEnemyDie
{
    // How an enemy dies on "Whole Lotta Neon" map

    public void OnDie()
    {
        Spawner.instance.RespawnEnemy(gameObject);

        PlayerKillCount.instance.IncreaseKillCount();

        EnableRagdoll();

        Destroy(gameObject, 3);
    }


    public void EnableRagdoll()
    {
        GetComponent<FireHead>().enabled = false;

        transform.DOShakePosition(3, 1, 10, 50, false, true).SetUpdate(true);
        transform.DOScale(0, 3);
    }
}
