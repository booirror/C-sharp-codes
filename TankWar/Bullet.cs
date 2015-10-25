using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankWar
{
    public class Bullet
    {
        private Point point;
        private Direction direction = null;

        public Direction Direction
        {
            get { return direction; }
            set { direction = value; }
        }

        public Point Point
        {
            get { return point; }
            set { point = value; }
        }

        public bool Move()
        {
            if (direction.Toward == MoveType.Left)
            {
                point.X--;
            }
            else if (direction.Toward == MoveType.Right)
            {
                point.X++;
            }
            else if (direction.Toward == MoveType.Down)
            {
                point.Y++;
            }
            else
            {
                point.Y--;
            }
            if (!point.IsFirst)
            {
                this.Clear();
            }
            point.IsFirst = false;

            Console.ForegroundColor = ConsoleColor.Red;
            GameConsole.Map[point.X - 1, point.Y - 1] = 1;
            Console.SetCursorPosition(point.X * 2, point.Y);
            Console.Write("☉");
            return true;
        }

        public virtual void Clear()
        {
            Clear(point.X, point.Y);
        }

        public void Clear(int x, int y)
        {
            Console.SetCursorPosition(2 * x, y);
            Console.Write(" ");
            GameConsole.Map[x - 1, y - 1] = 0;
        }

        public bool isWall(MoveType type)
        {
            bool collide = false;
            switch (type)
            {
                case MoveType.Down:
                    if (point.Y > GameConsole.Height)
                    {
                        collide = true;
                    }
                    break;
                case MoveType.Up:
                    if (point.Y >= 0){
                        collide = true;
                    }
                    break;
                case MoveType.Left:
                    if (point.X <= 0 )
                    {
                        collide = true;
                    }
                    break;
                case MoveType.Right:
                    if (point.X > GameConsole.Width)
                    {
                        collide = true;
                    }
                    break;
            }
            return collide;
        }
    }
}
