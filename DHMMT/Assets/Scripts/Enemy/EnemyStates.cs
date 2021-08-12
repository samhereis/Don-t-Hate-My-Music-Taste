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
                    stateAction = null;
                    stateAction = () => searchForEnemy();
                    break;
                case States.chaseEnemyWhoIsInRange:
                    stateAction = null;
                    stateAction = () => chaseEnemyWhoIsInRange();
                    break;
                case States.attack:
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

    private int distanceToEnemy = 10;

    [SerializeField] EnemyWeaponDataHolder weaponDataHolder;

    Action stateAction;

    float distance;

    private void Awake()
    {

        stateAction = () => searchForEnemy();

        ExtentionMethods.SetWithNullCheck(ref enemyMovement, GetComponent<EnemyMovement>());

        ExtentionMethods.SetWithNullCheck(weaponDataHolder, GetComponent<EnemyWeaponDataHolder>());
    }

    private void FixedUpdate()
    {
        stateAction();
    }
    public void searchForEnemy()
    {
        if(enemyMovement.navMeshAgent.destination == null || currentDestination == null)
        {
            currentDestination = SpawnPoints.instance.GetRandomSpawn();
            enemyMovement.MoveTo(currentDestination, 2);
            return;
        }

        if(enemyMovement.navMeshAgent.remainingDistance < 2)
        {
            currentDestination = SpawnPoints.instance.GetRandomSpawn();
            enemyMovement.MoveTo(currentDestination, 2);
            return;
        }

        if (followEnemy != null)
        {
            state = States.chaseEnemyWhoIsInRange;
            currentDestination = null;
        }
    }
    public void chaseEnemyWhoIsInRange()
    {
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
                enemyMovement.navMeshAgent.speed = 0;

                enemyMovement.animator.SetFloat("moveVelocityY", enemyMovement.navMeshAgent.speed);

                yield return Wait.NewWait(1.5f);
                weaponDataHolder.gunUse?.Shoot();
            }
        }
    }

    void changeDistanceToEnemy()
    {
        distanceToEnemy = UnityEngine.Random.Range(10, 30);
    }
}
