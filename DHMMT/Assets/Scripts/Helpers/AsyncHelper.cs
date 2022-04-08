using System;
using System.Threading.Tasks;

namespace Helpers
{
    public static class AsyncHelper
    {
        public static async Task Delay(float delay, Action callback = null)
        {
            await Task.Delay((int)delay * 1000);

            callback?.Invoke();
        }

        public static async Task Delay(int delay, Action callback = null)
        {
            await Task.Delay(delay);

            callback?.Invoke();
        }

        public static async Task Delay(Action callback = null)
        {
            await Task.Yield();

            callback?.Invoke();
        }
    }
}
