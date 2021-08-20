using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactorToXSpawn : MonoBehaviour
{
    public int numberOfCubes;

    public int distance;

    public List<GameObject> cubes;

    Vector3 lastLoc = new Vector3(0,0,0);

    void Start()
    {
        int i = 0;

        while(i < numberOfCubes)
        {
            Transform o = Instantiate(cubes[Random.Range(0, cubes.Count)], transform).transform;

            if(i == 0)
            {

            }
            else
            {
                lastLoc += new Vector3(0, 0, o.localPosition.z + o.localScale.z + distance);
            }

            o.localPosition = lastLoc;

            o.SetParent(MakeObjectsShake.instance.Reactors[Random.Range(0, MakeObjectsShake.instance.Reactors.Count)].transform, true);

            i++;
        }
    }
}
