using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Reactors
{
    public class ReactorToAxisSpawn : MonoBehaviour //TODO: complete this class
    {
        private enum Axis { X, Y, Z }

        [SerializeField] private Axis _axis;
        [SerializeField] private int _numberOfCubes;
        [SerializeField] private int _distance;
        [SerializeField] private List<GameObject> _cubes;
        [SerializeField] private Vector3 _lastLoc = new Vector3(0, 0, 0);

        private void Start()
        {
            if (_axis == Axis.X) SpawnX(); else SpawnZ();
        }

        private void SpawnX()
        {
            int i = 0;

            while (i < _numberOfCubes)
            {
                Transform o = Instantiate(_cubes[Random.Range(0, _cubes.Count)], transform).transform;

                if (i == 0)
                {

                }
                else
                {
                    _lastLoc += new Vector3(o.localPosition.x + o.localScale.x + _distance, 0, 0);
                }

                o.localPosition = _lastLoc;

                i++;
            }
        }

        private void SpawnZ()
        {
            int i = 0;

            while (i < _numberOfCubes)
            {
                Transform o = Instantiate(_cubes[Random.Range(0, _cubes.Count)], transform).transform;

                if (i == 0)
                {

                }
                else
                {
                    _lastLoc += new Vector3(0, 0, o.localPosition.z + o.localScale.z + _distance);
                }

                o.localPosition = _lastLoc;

                i++;
            }
        }
    }
}