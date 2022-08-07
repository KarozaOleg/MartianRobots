using MartianRobots.Model;
using System.Linq;

namespace MartianRobots.Service
{
    internal static class OutputService
    {
        public static string GetRobotStatus(Robot robot)
        {
            return $"{robot.Coordinates} {robot.Orientation.ToString().First()}{(robot.IsLost ? " LOST" : string.Empty)}";
        }
    }
}
