using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_F_OnEnemyDie : MonoBehaviour, IOnEnemyDie
{
    // How an enemy dies on "BeatleField" map

    public void OnDie()
    {
        Spawner.instance.RespawnEnemy(gameObject);

        EnableRagdoll();

        Destroy(gameObject, 3);
    }


    public void EnableRagdoll()
    {
        transform.DOShakePosition(3, 1, 10, 50, false, true).SetUpdate(true);
        transform.DOScale(0, 3);
    }
}
