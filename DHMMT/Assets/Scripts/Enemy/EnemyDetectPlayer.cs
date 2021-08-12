using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetectPlayer : MonoBehaviour
{
    Collider[] colliders;
    private float radius = 20;

    public LayerMask targetMask = 3;

    [SerializeField] EnemyStates enemyStates;

    private void Awake()
    {
        ExtentionMethods.SetWithNullCheck(ref enemyStates, GetComponent<EnemyStates>());
    }

    private void FixedUpdate()
    {
        colliders = Physics.OverlapSphere(transform.position, radius, targetMask);

        if (colliders.Length != 0)
        {
            enemyStates.followEnemy = colliders[0].transform;
        }
    }
}
