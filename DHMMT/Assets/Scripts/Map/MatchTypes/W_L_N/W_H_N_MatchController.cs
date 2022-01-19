using UnityEngine;

public class W_H_N_MatchController : MonoBehaviour, IMatchWinable, IMatchLoosable
{
    // Controll main players part on "W-H-N" map. "W_H_N_Page" controls UI part on this map

    private void OnEnable()
    {

    }

    public void Loose()
    {
        Spawner.instance.RepawnPlayer(gameObject, SpawnPoints.instance.GetRandomSpawn().transform);
        W_H_N_Page.instance.OnLoose();
    }

    public void Win()
    {
        W_H_N_Page.instance.OnWin();
    }

    private void OnDisable()
    {

    }
}