using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FileDelivery2_Client
{
    public class ItemClass
    {

        public static T[] GetDistinctValues<T>(T[] array)
        {
            List<T> tmp = new List<T>();

            for (int i = 0; i < array.Length; i++)
            {
                if (tmp.Contains(array[i]))
                    continue;
                tmp.Add(array[i]);
            }

            return tmp.ToArray();
        }

    }
}
