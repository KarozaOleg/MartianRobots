using MartianRobots.Interface;
using MartianRobots.Model;
using System;
using System.Linq;

namespace MartianRobots.Commands
{
    public class RobotCommandTurnLeft : IRobotCommand
    {
        private static RobotCommandTurnLeft Instance;

        private RobotCommandTurnLeft()
        {
        }

        public static RobotCommandTurnLeft GetInstance()
        {
            if (Instance == null)
                Instance = new RobotCommandTurnLeft();
            return Instance;
        }

        public void Execute(Robot robot, Map map)
        {
            var orientation = robot.Orientation - 1;

            if ((int)orientation < 0)
                orientation = Enum.GetValues(typeof(Orientation)).Cast<Orientation>().Last();

            robot.SetOrientation(orientation);
        }
    }
}
