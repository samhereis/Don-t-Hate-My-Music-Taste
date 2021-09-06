using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStates : MonoBehaviour
{
    public enum States { searchForEnemy, chaseEnemyWhoIsInRange, attack }
    States state { get { return State; } set
        {
            switch(value)
            {
                case States.searchForEnemy:
                        StopAllCoroutines();
                        stateAction = null;
                        stateAction = () => { if (followEnemy != null) { state = States.chaseEnemyWhoIsInRange; currentDestination = null; } };
                        StartCoroutine(searchForEnemy());
                    break;
                case States.chaseEnemyWhoIsInRange:
                        StopAllCoroutines();
                        stateAction = null;
                        stateAction = () => chaseEnemyWhoIsInRange();
                    break;
                case States.attack:
                        StopAllCoroutines();
                        stateAction = null;
                        stateAction = () => { transform.LookAt(followEnemy); };
                        StartCoroutine(attack());
                    break;
            }
            State = value;
        } }

    States State = States.searchForEnemy;

    [SerializeField]  EnemyMovement enemyMovement;

    public Transform followEnemy;

    public Transform currentDestination;

    public int distanceToEnemy = 10;

    [SerializeField] EnemyWeaponDataHolder weaponDataHolder;

    public float ShootRate;

    Action stateAction;

    float distance;

    private void Start()
    {
        state = States.searchForEnemy;

        ExtentionMethods.SetWithNullCheck(ref enemyMovement, GetComponent<EnemyMovement>());

        ExtentionMethods.SetWithNullCheck(weaponDataHolder, GetComponent<EnemyWeaponDataHolder>());
    }

    private void FixedUpdate()
    {
        stateAction();
    }
    public IEnumerator searchForEnemy()
    {

        if (currentDestination == null || enemyMovement.navMeshAgent.remainingDistance == 3)
        {
            currentDestination = SpawnPoints.instance.GetRandomSpawn();
            enemyMovement.MoveTo(currentDestination, 2);
        }

        yield return Wait.NewWait(10);
    }
    public void chaseEnemyWhoIsInRange()
    {
        if(followEnemy == null)
        {
            state = States.searchForEnemy;
        }

        distance = Vector3.Distance(this.transform.position, followEnemy.position);
        if (distance > distanceToEnemy)
        {
           enemyMovement.navMeshAgent.speed = 3f;
           enemyMovement.animator.SetFloat("moveVelocityY", enemyMovement.navMeshAgent.speed);
           enemyMovement.MoveTo(followEnemy, 3);
        }
        else if(distance < distanceToEnemy)
        {
           state = States.attack;
        }
    }
    public IEnumerator attack()
    {
        while(true)
        {
            if (followEnemy == null || Vector3.Distance(this.transform.position, followEnemy.position) > distanceToEnemy)
            {
                state = States.searchForEnemy;
                StopCoroutine(attack());
                yield break;
            }
            else
            {
                Debug.Log(Vector3.Distance(this.transform.position, followEnemy.position));

                enemyMovement.navMeshAgent.speed = 0;

                enemyMovement.animator.SetFloat("moveVelocityY", enemyMovement.navMeshAgent.speed);

                weaponDataHolder.gunUse?.Shoot();

                yield return Wait.NewWait(ShootRate);
            }
        }
    }

    void changeDistanceToEnemy()
    {
        distanceToEnemy = UnityEngine.Random.Range(10, 30);
    }
}
