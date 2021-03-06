using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_F_H_Exit : MonoBehaviour
{
    // Exit on "E-F-H" map

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerHealthData>())
        {
            E_F_H_Page.instance.OnWin();
        }
    }
}
