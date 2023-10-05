using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EditorHelper
{
    public class DisableOnAwake : MonoBehaviour
    {
        [SerializeField] private bool _disableOnAwake = true;

        private void Awake()
        {
            if (_disableOnAwake)
            {
                gameObject.SetActive(false);
            }
        }
    }
}