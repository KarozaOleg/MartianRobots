using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MartianRobots.Model
{
    public class Map
    {
        private int Width { get; }
        private int Height { get; }

        public Map(int width, int height)
        {
            if (width < 0)
                throw new ArgumentOutOfRangeException($"{nameof(width)} less than zero");
            if (height < 0)
                throw new ArgumentOutOfRangeException($"{nameof(height)} less than zero");

            Width = width;
            Height = height;
        }

        public bool IsNewCoordinatesInMap(Coordinates coordinates)
        {
            if (coordinates.X < 0 || coordinates.Y < 0)
                return false;
            if (coordinates.X > Width || coordinates.Y > Height)
                return false;

            return true;
        }
    }
}
