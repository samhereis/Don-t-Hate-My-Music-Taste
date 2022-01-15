using System.Collections.Generic;
using System.Threading.Tasks;

public static class ExtentionMethods
{
    public static async Task Delay(float delay)
    {
        await Task.Delay((int)delay * 1000);
    }

    public static async Task Delay()
    {
        await Task.Yield();
    }

    public static void RemoveNulls<T>(this List<T> list)
    {
        foreach (var item in list)
        {
            if(item == null)
            {
                list.Remove(item);
            }
        }
    }
}
