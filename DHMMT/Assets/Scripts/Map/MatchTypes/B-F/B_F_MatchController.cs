using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_F_MatchController : MonoBehaviour, IMatchLoosable, IMatchWinable
{
    // Controll main players part on "SH-TH" map. "SH-TH_Page" controls UI part on this map

    private void OnEnable()
    {
        PlayerKillCount.instance.SetInfinity();
    }

    public void Loose()
    {
        Spawner.instance.RepawnPlayer(SpawnPoints.instance.GetRandomSpawn());

        Destroy(gameObject);
    }

    public void Win()
    {
        B_F_Page.instance.OnWin();
    }

    private void OnDisable()
    {

    }
}
