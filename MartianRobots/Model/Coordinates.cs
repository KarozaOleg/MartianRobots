using System;

namespace MartianRobots.Model
{
    /// <summary>
    /// Represents a position on the map
    /// </summary>
    public class Coordinates
    {
        public int X { get; }
        public int Y { get; }

        public Coordinates(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return $"{X} {Y}";
        }

        public override bool Equals(object obj)
        {
            return obj is Coordinates coordinates &&
                   X == coordinates.X &&
                   Y == coordinates.Y;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }
    }
}
