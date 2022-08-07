using MartianRobots.Interface;
using MartianRobots.Model;
using System;
using System.Linq;

namespace MartianRobots.Commands
{
    public class RobotCommandTurnRight : IRobotCommand
    {
        private static RobotCommandTurnRight Instance;

        private RobotCommandTurnRight()
        {
        }

        public static RobotCommandTurnRight GetInstance()
        {
            if (Instance == null)
                Instance = new RobotCommandTurnRight();
            return Instance;
        }

        public void Execute(Robot robot, Map map)
        {
            var orientation = robot.Orientation + 1;
            var orientationMax = Enum.GetNames(typeof(Orientation)).Length;

            if ((int)orientation >= orientationMax)
                orientation = Enum.GetValues(typeof(Orientation)).Cast<Orientation>().First();

            robot.SetOrientation(orientation);
        }
    }
}
