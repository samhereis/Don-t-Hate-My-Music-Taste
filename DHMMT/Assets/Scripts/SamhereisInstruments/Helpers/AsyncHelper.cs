using System;
using System.Threading.Tasks;

namespace Helpers
{
    public static class AsyncHelper
    {
        public static async Task DelayAndDo(float delay, Action callback = null)
        {
            await Task.Delay((int)delay * 1000);
            callback?.Invoke();
        }

        public static async Task DoAndDelay(float delay, Action callback = null)
        {
            callback?.Invoke();
            await Task.Delay((int)delay * 1000);
        }

        public static async Task DelayAndDo(int delay, Action callback = null)
        {
            await Task.Delay(delay);
            callback?.Invoke();
        }

        public static async Task DoAndDelay(int delay, Action callback = null)
        {
            callback?.Invoke();
            await Task.Delay(delay);
        }

        public static async Task Delay(Action callback = null)
        {
            await Task.Yield();
            callback?.Invoke();
        }

        public static async Task Delay(float delay)
        {
            await Task.Delay(TimeSpan.FromSeconds(delay));
        }

        public static async Task Delay(int delay)
        {
            await Task.Delay(delay);
        }
    }
}
