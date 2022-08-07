using System;
using System.Collections.Generic;

namespace MartianRobots.Model
{
    public class Map
    {
        private int Width { get; }
        private int Height { get; }
        private HashSet<int> DropOffHashCodes { get; }

        /// <summary>
        /// Represents world map
        /// </summary>
        public Map(int width, int height)
        {
            Width = width;
            Height = height;
        }

        /// <summary>
        /// Check input coordinates in map boundaries
        /// </summary>
        /// <param name="coordinates"></param>
        /// <returns>returns true if coordinates out of map</returns>
        public bool IsCoordinatesOutOfMap(Coordinates coordinates)
        {
            if (coordinates.X < 0 || coordinates.Y < 0)
                return true;
            if (coordinates.X > Width || coordinates.Y > Height)
                return true;

            return false;
        }

        /// <summary>
        /// Adding information about robot drop off move
        /// </summary>
        /// <param name="coordinates"></param>
        /// <param name="orientation"></param>
        public void SetAddDropOffMove(Coordinates coordinates, Orientation orientation)
        {
            var moveHashCode = ReturnMoveHashCode(coordinates, orientation);
            if (DropOffHashCodes.Contains(moveHashCode) == false)
                DropOffHashCodes.Add(moveHashCode);
        }

        /// <summary>
        /// Return is move will drop off robot from map
        /// </summary>
        /// <param name="coordinates"></param>
        /// <param name="orientation"></param>
        /// <returns></returns>
        public bool GetIsMoveWillDropOff(Coordinates coordinates, Orientation orientation)
        {
            var moveHashCode = ReturnMoveHashCode(coordinates, orientation);
            return DropOffHashCodes.Contains(moveHashCode);
        }

        private int ReturnMoveHashCode(Coordinates coordinates, Orientation orientation)
        {
            return HashCode.Combine(coordinates.GetHashCode(), orientation.GetHashCode());
        }
    }
}
