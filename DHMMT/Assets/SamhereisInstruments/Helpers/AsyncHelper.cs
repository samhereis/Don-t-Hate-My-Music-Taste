using UnityEngine;

namespace Helpers
{
    public static class AsyncHelper
    {
        public static async Awaitable Delay()
        {
            await Awaitable.NextFrameAsync();
        }

        public static async Awaitable NextFrame()
        {
            await Awaitable.NextFrameAsync();
        }

        public static async Awaitable DelayFloat(float delay)
        {
            await Awaitable.WaitForSecondsAsync(delay);
        }

        public static async Awaitable DelayInt(int delay)
        {
            float duration = delay / 1000;
            await Awaitable.WaitForSecondsAsync(duration);
        }
    }
}