using UnityEngine;

public class WHN_MatchController : MonoBehaviour, IMatchController
{
    void OnEnable()
    {
        GameplayUI.instance.Enable(GameplayUI.instance.WH_L_N_page);
        SecondsCount.instance.Stop();
        SecondsCount.instance.Beggin(1);
    }

    public void Loose()
    {
        Spawner.instance.RepawnPlayer(gameObject, SpawnPoints.instance.GetRandomSpawn().transform);
        SecondsCount.instance.Stop();
        SecondsCount.instance.Beggin(1);
    }
}