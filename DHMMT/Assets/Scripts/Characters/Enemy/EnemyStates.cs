using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStates : MonoBehaviour
{
    // TODO: Make every state a separate script for better controll

    // Controlls an enemie's state
    public enum States { searchForEnemy, chaseEnemyWhoIsInRange, attack }

    States state { get { return State; } set
        {
            switch(value)
            {
                case States.searchForEnemy:
                        StopAllCoroutines();
                        _stateAction = null;
                        _stateAction = () => { if (FollowedEnemy != null) { state = States.chaseEnemyWhoIsInRange; CurrentDestination = null; } };
                        searchForEnemy();
                    break;
                case States.chaseEnemyWhoIsInRange:
                        StopAllCoroutines();
                        _stateAction = null;
                        _stateAction = () => chaseEnemyWhoIsInRange();
                    break;
                case States.attack:
                        StopAllCoroutines();
                        _stateAction = null;
                        _stateAction = () => { transform.LookAt(FollowedEnemy); };
                        attack();
                    break;
            }
            State = value;
        } }

    States State = States.searchForEnemy;

    [SerializeField] private EnemyMovement _enemyMovement;

    public Transform FollowedEnemy;

    public Transform CurrentDestination;

    public int DistanceToEnemy = 10;

    [SerializeField] private EnemyWeaponDataHolder _weaponDataHolder;

    public float ShootRate;

    private Action _stateAction;

    private float _distance;

    private void Start()
    {
        state = States.searchForEnemy;

        _enemyMovement ??= GetComponent<EnemyMovement>();

        _weaponDataHolder ??= GetComponent<EnemyWeaponDataHolder>();
    }

    private void FixedUpdate()
    {
        _stateAction();
    }

    public void searchForEnemy()
    {

    }

    public void chaseEnemyWhoIsInRange()
    {
        if(FollowedEnemy == null)
        {
            state = States.searchForEnemy;
        }

        _distance = Vector3.Distance(this.transform.position, FollowedEnemy.position);
        if (_distance > DistanceToEnemy)
        {
           _enemyMovement.NavMeshAgentOfEnemy.speed = 3f;
           _enemyMovement.AnimatorOfEnemy.SetFloat("moveVelocityY", _enemyMovement.NavMeshAgentOfEnemy.speed);
           _enemyMovement.MoveTo(FollowedEnemy, 3);
        }
        else if(_distance < DistanceToEnemy)
        {
           state = States.attack;
        }
    }
    public void attack()
    {
        while(true)
        {
            if (FollowedEnemy == null || Vector3.Distance(this.transform.position, FollowedEnemy.position) > DistanceToEnemy)
            {
                state = States.searchForEnemy;
            }
            else
            {
                Debug.Log(Vector3.Distance(this.transform.position, FollowedEnemy.position));

                _enemyMovement.NavMeshAgentOfEnemy.speed = 0;

                _enemyMovement.AnimatorOfEnemy.SetFloat("moveVelocityY", _enemyMovement.NavMeshAgentOfEnemy.speed);

                _weaponDataHolder.GunUseComponent?.Shoot();
            }
        }
    }

    void changeDistanceToEnemy()
    {
        DistanceToEnemy = UnityEngine.Random.Range(10, 30);
    }
}
