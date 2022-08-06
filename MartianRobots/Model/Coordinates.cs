using System;

namespace MartianRobots.Model
{
    public class Coordinates
    {
        public int X { get; }
        public int Y { get; }

        public Coordinates(int x, int y)
        {
            if (x < 0)
                throw new ArgumentOutOfRangeException($"{nameof(x)} less than zero");
            if (y < 0)
                throw new ArgumentOutOfRangeException($"{nameof(y)} less than zero");
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return $"{X} {Y}";
        }
    }
}
