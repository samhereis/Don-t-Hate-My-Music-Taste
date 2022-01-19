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

    public async static void RemoveNulls<T>(this List<T> list)
    {
        List<T> listTemp = new List<T>();
        listTemp.AddRange(list);

        foreach (var item in listTemp)
        {
            await ExtentionMethods.Delay();

            if(item == null)
            {
                list.Remove(item);
            }
        }
    }
}
