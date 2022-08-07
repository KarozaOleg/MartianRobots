using MartianRobots.Model;
using System.Linq;

namespace MartianRobots.Service
{
    public static class OutputService
    {
        public static string GetRobotStatus(Robot robot)
        {
            return $"{robot.Coordinates} {robot.Orientation.ToString().First()}{(robot.IsLost ? " LOST" : string.Empty)}";
        }
    }
}
