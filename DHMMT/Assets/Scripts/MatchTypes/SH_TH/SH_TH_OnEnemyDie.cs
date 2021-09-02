using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SH_TH_OnEnemyDie : MonoBehaviour, IOnEnemyDie
{
    void OnEnable()
    {
        
    }

    public void OnDie()
    {
        Spawner.instance.enemies.Remove(gameObject);

        if(Spawner.instance.enemies.Count == 0)
        {
            SH_TH_Page.instance.OnWin();
        }

        PlayerKillCount.instance.KillCount = Spawner.instance.enemies.Count;

        Destroy(gameObject);
    }
}
