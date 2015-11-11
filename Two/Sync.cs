using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadTs
{
    class Sync
    {
        public static void Test()
        {
            ArrayList myAL = new ArrayList();
            myAL.Add("The");
            myAL.Add("quick");
            myAL.Add("fox");

            ArrayList mys = ArrayList.Synchronized(myAL);

            Console.WriteLine(mys.IsSynchronized);
        }
    }
}
