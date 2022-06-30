using UnityEngine;

namespace Samhereis.DI
{
    public class DITest : MonoBehaviour, IDIDependent
    {
        [DI][SerializeField] private Vector3 _value;

        [ContextMenu(nameof(Load))] public async void Load()
        {
            await (this as IDIDependent).LoadDependencies();
        }
    }
}