using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactorToAxisSpawn : MonoBehaviour
{
    // Spawn reactors to certain direction

    public int NumberOfCubes;

    public enum Axis { X, Y, Z }

    public Axis axis;

    public int Distance;

    public List<GameObject> Cubes;

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

        while (i < NumberOfCubes)
        {
            Transform o = Instantiate(Cubes[Random.Range(0, Cubes.Count)], transform).transform;

            if (i == 0)
            {

            }
            else
            {
                lastLoc += new Vector3(o.localPosition.x + o.localScale.x + Distance, 0, 0);
            }

            o.localPosition = lastLoc;

            if(Shake) o.SetParent(MakeObjectsShake.instance.Reactors[o.GetComponent<OnStartSetParentTo>().MakeObjectsShakeReactorIndex].transform, true);

            i++;
        }
    }
    void SpawnZ()
    {
        int i = 0;

        while (i < NumberOfCubes)
        {
            Transform o = Instantiate(Cubes[Random.Range(0, Cubes.Count)], transform).transform;

            if (i == 0)
            {

            }
            else
            {
                lastLoc += new Vector3(0, 0, o.localPosition.z + o.localScale.z + Distance);
            }

            o.localPosition = lastLoc;

            if (Shake) o.SetParent(MakeObjectsShake.instance.Reactors[o.GetComponent<OnStartSetParentTo>().MakeObjectsShakeReactorIndex].transform, true);

            i++;
        }
    }
}
