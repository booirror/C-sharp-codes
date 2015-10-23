
namespace TankWar
{
    public class Direction
    {
        private MoveType toward;

        public MoveType Toward
        {
            get { return toward; }
            set { toward = value; }
        }

        public Direction(MoveType direct)
        {
            this.toward = direct;
        }
    }
}