using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactorToXSpawn : MonoBehaviour
{
    public int numberOfCubes;

    public enum Axis { X, Y, Z }

    public Axis axis;

    public int distance;

    public List<GameObject> cubes;

    public bool Shake = true;

    Vector3 lastLoc = new Vector3(0,0,0);

    void Start()
    {
        if (axis == Axis.X)
        {
            SpawnX();
        }
        else
        {
            SpawnZ();
        }
    }

    void SpawnX()
    {
        int i = 0;

        while (i < numberOfCubes)
        {
            Transform o = Instantiate(cubes[Random.Range(0, cubes.Count)], transform).transform;

            if (i == 0)
            {

            }
            else
            {
                lastLoc += new Vector3(o.localPosition.x + o.localScale.x + distance, 0, 0);
            }

            o.localPosition = lastLoc;

            if(Shake) o.SetParent(MakeObjectsShake.instance.Reactors[Random.Range(0, MakeObjectsShake.instance.Reactors.Count)].transform, true);

            i++;
        }
    }
    void SpawnZ()
    {
        int i = 0;

        while (i < numberOfCubes)
        {
            Transform o = Instantiate(cubes[Random.Range(0, cubes.Count)], transform).transform;

            if (i == 0)
            {

            }
            else
            {
                lastLoc += new Vector3(0, 0, o.localPosition.z + o.localScale.z + distance);
            }

            o.localPosition = lastLoc;

            if (Shake) o.SetParent(MakeObjectsShake.instance.Reactors[Random.Range(0, MakeObjectsShake.instance.Reactors.Count)].transform, true);

            i++;
        }
    }
}
