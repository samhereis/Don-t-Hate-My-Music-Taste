using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_F_H_OnEnemyDie : MonoBehaviour, IOnEnemyDie
{
    // TODO: finish "EnableRagdoll()"

    // How an enemy dies on "Escape from Haters" map

    public void OnDie()
    {
        StartCoroutine(Respwn());

        PlayerKillCount.instance.IncreaseKillCount();

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

    IEnumerator Respwn()
    {
        yield return Wait.NewWaitRealTime(Random.Range(2, 4));
        Spawner.instance.SpawnEnemy(SpawnPoints.instance.GetRandomSpawn().transform);
    }
}
