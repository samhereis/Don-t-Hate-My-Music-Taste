using System.Collections;
using UnityEngine;

public class AddPlayerOnAwake : MonoBehaviour
{
    // Adds main player to enemy so that enemy chases main player right away without serching for main player

    private void Awake()
    {
        StartCoroutine(AddPlayer());
    }

    private IEnumerator AddPlayer()
    {
        while (true)
        {
            GetComponent<EnemyStates>().FollowedEnemy = PlayerHealthData.instance.gameObject.transform;

            yield return Wait.NewWait(5);
        }
    }
}
