using System.Collections.Generic;
using System.Threading.Tasks;

namespace Helpers
{
    public static class AsyncHelper
    {
        public static async Task Delay(float delay)
        {
            await Task.Delay((int)delay * 1000);
        }

        public static async Task Delay()
        {
            await Task.Yield();
        }
    }
}
