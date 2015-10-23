using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankWar
{
    public struct Point
    {
        private int x;
        private int y;
        private bool isFirst;

        public int X
        {
            get { return x; }
            set { x = value; }
        }

        public int Y
        {
            set { y = value; }
            get { return y; }
        }

        public bool IsFirst
        {
            get { return isFirst; }
            set { isFirst = value; }
        }

        public Point(int x, int y, bool isFirst)
        {
            this.x = x;
            this.y = y;
            this.isFirst = isFirst;
        }
    }
}
