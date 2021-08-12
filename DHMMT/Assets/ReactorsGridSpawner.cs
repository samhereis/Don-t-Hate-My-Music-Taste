using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ReactorsGridSpawner : MonoBehaviour
{
    public GameObject B, NB, M, H;

    public List<GameObject> Prefs;
    public List<Vector2> dick = new List<Vector2>()
    {
        new Vector2 (0, 0   ),   new Vector2 (20, 0   ),   new Vector2 (40, 0   ),   new Vector2 (60, 0   ),   new Vector2 (80, 0   ),   new Vector2 (100, 0  ),
        new Vector2 (0, 20  ),   new Vector2 (20, 20  ),   new Vector2 (40, 20  ),   new Vector2 (60, 20  ),   new Vector2 (80, 20  ),   new Vector2 (100, 20 ),
        new Vector2 (0, 40  ),   new Vector2 (20, 40  ),   new Vector2 (40, 40  ),   new Vector2 (60, 40  ),   new Vector2 (80, 40  ),   new Vector2 (100, 40 ),
        new Vector2 (0, 60  ),   new Vector2 (20, 60  ),   new Vector2 (40, 60  ),   new Vector2 (60, 60  ),   new Vector2 (80, 60  ),   new Vector2 (100, 60 ),
        new Vector2 (0, 80  ),   new Vector2 (20, 80  ),   new Vector2 (40, 80  ),   new Vector2 (60, 80  ),   new Vector2 (80, 80  ),   new Vector2 (100, 80 ),
        new Vector2 (0, 100 ),   new Vector2 (20, 100 ),   new Vector2 (40, 100 ),   new Vector2 (60, 100 ),   new Vector2 (80, 100 ),   new Vector2 (100, 100),
        new Vector2 (0, 120 ),   new Vector2 (20, 120 ),   new Vector2 (40, 120 ),   new Vector2 (60, 120 ),   new Vector2 (80, 120 ),   new Vector2 (100, 120),
        new Vector2 (0, 140 ),   new Vector2 (20, 140 ),   new Vector2 (40, 140 ),   new Vector2 (60, 140 ),   new Vector2 (80, 140 ),   new Vector2 (100, 140),
    };                                                                                            

    private void Awake()
    {
        Spawn(B, Prefs[0],  12);
        Spawn(NB, Prefs[1], 12);
        Spawn(M, Prefs[2],  12);
        Spawn(H, Prefs[3],  12);
    }
    void Spawn(GameObject Holder, GameObject Pref, int NumberOfCubes)
    {
        for(int i =0; i <= NumberOfCubes; i++)
        {
            Vector2 t = dick[Random.Range(0, dick.Count)];

            GameObject obj = Instantiate(Pref, Holder.transform);
            obj.transform.localPosition = new Vector3(t.y, 0, t.x);

            dick.Remove(t);
        }
    }
}
