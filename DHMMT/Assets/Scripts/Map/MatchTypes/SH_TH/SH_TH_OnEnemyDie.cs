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

        Spawner.instance.RespawnEnemy(gameObject);

        if (Spawner.instance.Enemies.Count + Spawner.instance.EnemiesReserve.Count == 0)
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
