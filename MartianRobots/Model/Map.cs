﻿using System;

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
    }
}
