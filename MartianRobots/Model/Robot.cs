using System;

namespace MartianRobots.Model
{
    public class Robot
    {
        public int Id { get; }
        public Direction Direction { get; private set; }
        public Coordinates Coordinates { get; private set; }
        public bool IsLost { get; private set; }

        public Robot(int id, Coordinates coordinates, Direction direction)
        {
            Id = id;
            Coordinates = coordinates ?? throw new ArgumentNullException(nameof(coordinates));
            Direction = direction;
        }

        public void SetDirection(Direction direction)
        {
            Direction = direction;
        }

        public void SetCoordinates(Coordinates coordinates)
        {
            Coordinates = coordinates ?? throw new ArgumentNullException(nameof(coordinates));
        }

        public void SetIsLostMarkToTrue()
        {
            IsLost = true;
        }
    }
}
