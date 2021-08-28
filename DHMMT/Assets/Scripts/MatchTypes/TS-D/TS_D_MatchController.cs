using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TS_D_MatchController : MonoBehaviour, IMatchWinable, IMatchLosable
{
    void OnEnable()
    {
        GameplayUI.instance.Enable(GameplayUI.instance.TS_D_page);
        GameplayUI.instance.Disable(GameplayUI.instance.Camera);
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

    private void OnDisable()
    {
        GameplayUI.instance.Enable(GameplayUI.instance.Camera);
    }
}
