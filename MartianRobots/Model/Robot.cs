using System;

namespace MartianRobots.Model
{
    /// <summary>
    /// Represent a robot model - current orientation, coordinates
    /// </summary>
    public class Robot
    {
        public int Id { get; }
        public Orientation Orientation { get; private set; }
        public Coordinates Coordinates { get; private set; }
        /// <summary>
        /// Shows current status of robot, lost means dropped out from map
        /// </summary>
        public bool IsLost { get; private set; }

        public Robot(int id, Coordinates coordinates, Orientation orientation)
        {
            Id = id;
            SetCoordinates(coordinates);
            SetOrientation(orientation);
        }

        public void SetOrientation(Orientation orientation)
        {
            Orientation = orientation;
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
