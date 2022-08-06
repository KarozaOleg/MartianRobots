using System;
using System.Collections.Generic;

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
            Coordinates = coordinates;
        }

        public void SetIsLostToTrue()
        {
            IsLost = true;
        }

        public override string ToString()
        {
            return $"{Coordinates} {Direction.ToString()[0]}{(IsLost ? " LOST" : string.Empty)}";
        }
    }
}
