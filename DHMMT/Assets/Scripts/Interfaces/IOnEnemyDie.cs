using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IOnEnemyDie
{
    // Bluepring of how an enemy dies

    public void OnDie();

    void EnableRagdoll();
}
