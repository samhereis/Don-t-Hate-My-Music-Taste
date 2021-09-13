using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TS_D_OnEnemyDie : MonoBehaviour, IOnEnemyDie
{
    public void OnDie()
    {
        Spawner.instance.SpawnEnemy(SpawnPoints.instance.GetRandomSpawn().transform);

        PlayerKillCount.instance.IncreaseKillCount();

        SecondsCount.instance.IncreaseSeconds(8);

        AnimationStatics.NormalShake(SecondsCount.instance.transform, 2);

        Spawner.instance.Enemies.Remove(gameObject);

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
