using MartianRobots.Interface;
using MartianRobots.Model;
using System;

namespace MartianRobots.Commands
{
    public class RobotCommandMoveForward : IRobotCommand
    {
        private static RobotCommandMoveForward Instance;

        private RobotCommandMoveForward()
        {
        }

        public static RobotCommandMoveForward GetInstance()
        {
            if (Instance == null)
                Instance = new RobotCommandMoveForward();
            return Instance;
        }

        public void Execute(Robot robot, Map map)
        {
            if (map.GetIsMoveWillDropOff(robot.Coordinates, robot.Orientation))
                return;

            var coordinateNewX = robot.Coordinates.X;
            var coordinateNewY = robot.Coordinates.Y;
            switch (robot.Orientation)
            {
                case Orientation.North:
                    coordinateNewY = robot.Coordinates.Y + 1;
                    break;

                case Orientation.East:
                    coordinateNewX = robot.Coordinates.X + 1;
                    break;

                case Orientation.South:
                    coordinateNewY = robot.Coordinates.Y - 1;
                    break;

                case Orientation.West:
                    coordinateNewX = robot.Coordinates.X - 1;
                    break;

                default:
                    throw new ArgumentException($"wrong orientation - {robot.Orientation}");

            }
            var coordinates = new Coordinates(coordinateNewX, coordinateNewY);
            if (map.IsCoordinatesOutOfMap(coordinates))
            {
                map.SetAddDropOffMove(robot.Coordinates, robot.Orientation);
                robot.SetIsLostMarkToTrue();
            }
            else
                robot.SetCoordinates(coordinates);
        }
    }
}
