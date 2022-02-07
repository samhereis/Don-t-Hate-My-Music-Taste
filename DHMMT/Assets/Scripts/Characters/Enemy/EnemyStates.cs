using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStates : MonoBehaviour
{
    // TODO: Make every state a separate script for better controll

    // Controlls an enemie's state

    [SerializeField] private EnemyMovement _enemyMovement;

    public Transform FollowedEnemy;

    public Transform CurrentDestination;

    public int DistanceToEnemy = 10;

    public float ShootRate;

    private Action _stateAction;

    private float _distance;

    private void Start()
    {
        _enemyMovement ??= GetComponent<EnemyMovement>();
    }

    private void FixedUpdate()
    {
        if(_stateAction != null) _stateAction();
    }

    public void searchForEnemy()
    {

    }

    public void chaseEnemyWhoIsInRange()
    {

    }

    public void attack()
    {

    }

    void changeDistanceToEnemy()
    {
        DistanceToEnemy = UnityEngine.Random.Range(10, 30);
    }
}
