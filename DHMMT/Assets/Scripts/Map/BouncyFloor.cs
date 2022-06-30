using Helpers;
using Samhereis.Helpers;
using UnityEngine;

namespace Gameplay
{
    public class BouncyFloor : MonoBehaviour
    {
        private async void OnTriggerEnter(Collider other)
        {
            await AsyncHelper.Delay(() =>
            {
                other.GetComponent<PlayerJump>()?.PerformJump(1);
            });
        }

        private async void OnCollisionEnter(Collision other)
        {
            await AsyncHelper.Delay(() =>
            {
                other.collider.GetComponent<PlayerJump>()?.PerformJump(1);
            });
        }
    }
}