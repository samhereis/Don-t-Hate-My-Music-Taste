using UnityEngine;

namespace DI
{
    public abstract class FactoryDI : MonoBehaviour
    {
        public abstract void Create();

        public abstract void DestroyDi();
    }
}