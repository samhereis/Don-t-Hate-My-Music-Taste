using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Identifiers
{
    public class IdentifierBase : MonoBehaviour
    {
        public T TryGet<T>()
        {
            return GetComponentInChildren<T>(true);
        }
    }
}
