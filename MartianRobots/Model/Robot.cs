using System;

namespace MartianRobots.Model
{
    /// <summary>
    /// Represent a robot model, current direction, coordinates
    /// </summary>
    public class Robot
    {
        public int Id { get; }
        public Orientation Orientation { get; private set; }
        public Coordinates Coordinates { get; private set; }
        public bool IsLost { get; private set; }

        public Robot(int id, Coordinates coordinates, Orientation orientation)
        {
            Id = id;
            Coordinates = coordinates ?? throw new ArgumentNullException(nameof(coordinates));
            Orientation = orientation;
        }

        public void SetOrientation(Orientation direction)
        {
            Orientation = direction;
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
