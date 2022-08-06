using MartianRobots.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MartianRobots.Service
{
    public class RobotCommandService
    {
        private Dictionary<int, Robot> RobotsById { get; }
        private List<RobotCommand> RobotsCommands { get; }

        public RobotCommandService(List<Robot> robots, List<RobotCommand> robotsCommands)
        {
            RobotsById = ReturnRobotsById(robots) ?? throw new ArgumentNullException(nameof(robots));
            RobotsCommands = robotsCommands ?? throw new ArgumentNullException(nameof(robotsCommands));
        }

        private Dictionary<int, Robot> ReturnRobotsById(List<Robot> robots)
        {
            return robots
                .GroupBy(r => r.Id)
                .ToDictionary(r => r.Key, r => r.Single());
        }

        public void LaunchAllRobots()
        {
            foreach (var robotCommands in RobotsCommands)
            {
                if (RobotsById.ContainsKey(robotCommands.Id) == false)
                    throw new KeyNotFoundException($"{robotCommands.Id} in RobotsById");

                var robot = RobotsById[robotCommands.Id];
                foreach (var command in robotCommands.Commands)
                    HandleRobotCommand(robot, command);                
            }
        }

        private void HandleRobotCommand(Robot robot, Command command)
        {
            switch (command)
            {
                case Command.Left:
                    {
                        var robotDirectionNew = ReturnRobotDirectionAfterTurnLeft(robot.Direction);
                        robot.SetDirection(robotDirectionNew);
                        break;
                    }

                case Command.Right:
                    {
                        var robotDirectionNew = ReturnRobotDirectionAfterTurnRight(robot.Direction);
                        robot.SetDirection(robotDirectionNew);
                        break;
                    }

                case Command.Forward:
                    var robotCoordinatesNew = ReturnNewCoordinates(robot.Coordinates, robot.Direction);
                    robot.SetCoordinates(robotCoordinatesNew);
                    break;

                default:
                    throw new ArgumentException($"wrong robot command - {command}");
            }
        }

        private Direction ReturnRobotDirectionAfterTurnLeft(Direction robotDirectionCurrent)
        {
            var robotDirectionNew = robotDirectionCurrent - 1;

            if ((int)robotDirectionNew < 0)
                robotDirectionNew = Enum.GetValues(typeof(Direction)).Cast<Direction>().Last();

            return robotDirectionNew;
        }

        private Direction ReturnRobotDirectionAfterTurnRight(Direction robotDirectionCurrent)
        {
            var robotDirectionNew = robotDirectionCurrent + 1;
            var robotDirectionMax = Enum.GetNames(typeof(Direction)).Length;

            if ((int)robotDirectionNew >= robotDirectionMax)
                robotDirectionNew = Enum.GetValues(typeof(Direction)).Cast<Direction>().First();

            return robotDirectionNew;
        }

        private Coordinates ReturnNewCoordinates(Coordinates coordinatesCurrent, Direction direction)
        {
            var coordinateNewX = coordinatesCurrent.X;
            var coordinateNewY = coordinatesCurrent.Y;
            switch (direction)
            {
                case Direction.North:
                    coordinateNewY = coordinatesCurrent.Y + 1;
                    break;

                case Direction.East:
                    coordinateNewX = coordinatesCurrent.X + 1;
                    break;

                case Direction.South:
                    coordinateNewY = coordinatesCurrent.Y - 1;
                    break;

                case Direction.West:
                    coordinateNewX = coordinatesCurrent.X - 1;
                    break;

                default:
                    throw new ArgumentException($"wrong robot direction - {direction}");

            }
            return new Coordinates(coordinateNewX, coordinateNewY);
        }
    }
}
