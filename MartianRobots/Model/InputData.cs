using System;
using System.Collections.Generic;

namespace MartianRobots.Model
{
    /// <summary>
    /// Represent input data for application
    /// </summary>
    public class InputData
    {
        public int MapWidth { get; }
        public int MapHeight { get; }
        public List<Robot> Robots { get; }
        public List<RobotCommands> RobotsCommands { get; }

        public InputData(int mapWidth, int mapHeight, List<Robot> robots, List<RobotCommands> robotsCommands)
        {
            if (mapWidth < 1)
                throw new ArgumentOutOfRangeException($"{nameof(mapWidth)} less than 1");
            if (mapHeight < 1)
                throw new ArgumentOutOfRangeException($"{nameof(mapHeight)} less than 1");

            MapWidth = mapWidth;
            MapHeight = mapHeight;
            Robots = robots ?? throw new ArgumentNullException(nameof(robots));
            RobotsCommands = robotsCommands ?? throw new ArgumentNullException(nameof(robotsCommands));
        }
    }
}
