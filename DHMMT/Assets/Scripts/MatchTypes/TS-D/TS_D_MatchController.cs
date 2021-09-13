using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TS_D_MatchController : MonoBehaviour, IMatchWinable, IMatchLoosable
{
    // Controll main players part on "SH-TH" map. "SH-TH_Page" controls UI part on this map

    private void OnEnable()
    {
        GameplayUI.instance.Enable(GameplayUI.instance.TS_D_page);

        GetComponent<PlayerJump>().DoubleJumpable = false;
    }

    private void OnDisable()
    {
        GameplayUI.instance.Disable(GameplayUI.instance.TS_D_page);
    }

    public void Loose()
    {
        Spawner.instance.RepawnPlayer(gameObject, SpawnPoints.instance.GetRandomSpawn().transform);
        TS_D_Page.instance.OnLoose();
    }

    public void Win()
    {
        TS_D_Page.instance.OnWin();
    }
}
