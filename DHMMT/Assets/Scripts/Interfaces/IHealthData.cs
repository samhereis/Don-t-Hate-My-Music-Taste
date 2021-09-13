using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealthData
{
    // Health data of a humanoid

    bool IsAlive { get; set; }
    float Health { get; set; }
    float MaxHealth { get; set; }

    void TakeDamage(float damage);
}
