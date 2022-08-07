using MartianRobots.Commands;
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
            MapWidth = mapWidth;
            MapHeight = mapHeight;
            Robots = robots ?? throw new ArgumentNullException(nameof(robots));
            RobotsCommands = robotsCommands ?? throw new ArgumentNullException(nameof(robotsCommands));
        }
    }
}
