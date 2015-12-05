using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTest
{
    class Shape
    {
        public virtual void foo()
        {
            Console.WriteLine("call Shape");
        }
    }
    class Rectangle : Shape
    {
        public override void foo()
        {
            Console.WriteLine("call Rectangle");
        }
    }

    class Square : Rectangle
    {
        public override void foo()
        {
            Console.WriteLine("call square");
        }
    }

    class VirtualTest
    {
        static public void Test()
        {
            Shape a = new Shape();
            a.foo();
            Shape b = new Rectangle();
            b.foo();
            Shape c = new Square();
            c.foo();


        }

    }
}
