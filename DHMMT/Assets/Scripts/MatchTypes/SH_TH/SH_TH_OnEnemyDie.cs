using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SH_TH_OnEnemyDie : MonoBehaviour, IOnEnemyDie
{
    // TODO: finish "EnableRagdoll()"

    // How an enemy dies on "Shangri The" map

    public void OnDie()
    {
        Spawner.instance.Enemies.Remove(gameObject);

        if(Spawner.instance.EnemiesReserve.Count > 0)
        {
            var obj = Spawner.instance.EnemiesReserve[Random.Range(0, Spawner.instance.EnemiesReserve.Count)];

            obj.SetActive(true);

            obj.transform.position = SpawnPoints.instance.GetRandomSpawn().position;
        }

        if(Spawner.instance.Enemies.Count + Spawner.instance.EnemiesReserve.Count == 0)
        {
            SH_TH_Page.instance.OnWin();
        }

        PlayerKillCount.instance.KillCount = Spawner.instance.Enemies.Count + Spawner.instance.EnemiesReserve.Count;

        EnableRagdoll();

        Destroy(gameObject, 5);
    }

    public void EnableRagdoll()
    {
        GetComponent<Animator>().enabled = false;

        GetComponent<EnemyStates>().enabled = false;

        Destroy(GetComponent<HumanoidEquipWeaponData>().CurrentWeapon);
    }
}
