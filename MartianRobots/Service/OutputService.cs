using MartianRobots.Model;
using System;
using System.Linq;

namespace MartianRobots.Service
{
    public static class OutputService
    {
        /// <summary>
        /// Collect status of robot and represent it as a string
        /// </summary>
        /// <param name="robot"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static string GetRobotStatus(Robot robot)
        {
            if (robot == null)
                throw new ArgumentNullException(nameof(robot));

            return $"{robot.Coordinates} {robot.Orientation.ToString().First()}{(robot.IsLost ? " LOST" : string.Empty)}";
        }
    }
}
