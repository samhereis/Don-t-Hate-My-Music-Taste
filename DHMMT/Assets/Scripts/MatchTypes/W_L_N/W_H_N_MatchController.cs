using UnityEngine;

public class W_H_N_MatchController : MonoBehaviour, IMatchWinable, IMatchLosable
{
    void OnEnable()
    {
        GameplayUI.instance.Enable(GameplayUI.instance.WH_L_N_page);
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
        GameplayUI.instance.Disable(GameplayUI.instance.WH_L_N_page);
    }
}