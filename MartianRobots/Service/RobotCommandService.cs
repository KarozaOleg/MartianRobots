using MartianRobots.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MartianRobots.Service
{
    public class RobotCommandService
    {
        private Map Map { get; }
        private Dictionary<int, Robot> RobotsById { get; }
        private List<RobotCommand> RobotsCommands { get; }

        public RobotCommandService(Map map, List<Robot> robots, List<RobotCommand> robotsCommands)
        {
            Map = map ?? throw new ArgumentException(nameof(map));
            RobotsById = ReturnRobotsById(robots) ?? throw new ArgumentNullException(nameof(robots));
            RobotsCommands = robotsCommands ?? throw new ArgumentNullException(nameof(robotsCommands));
        }

        private Dictionary<int, Robot> ReturnRobotsById(List<Robot> robots)
        {
            if (robots == null)
                throw new ArgumentNullException(nameof(robots));

            return robots
                .GroupBy(r => r.Id)
                .ToDictionary(r => r.Key, r => r.Single());
        }

        public void LaunchAllRobots()
        {
            var badPoints = new HashSet<int>();
            foreach (var robotCommands in RobotsCommands)
            {
                if (RobotsById.ContainsKey(robotCommands.Id) == false)
                    throw new KeyNotFoundException($"{robotCommands.Id} in RobotsById");

                var robot = RobotsById[robotCommands.Id];
                foreach (var command in robotCommands.Commands)
                {
                    if (robot.IsLost)
                        continue;

                    switch (command.Type)
                    {
                        case CommandType.Turning:
                            var directionNew = ReturnNewDirection(robot.Direction, command.Value);
                            robot.SetDirection(directionNew);
                            break;

                        case CommandType.Moving:
                            var movingHashCode = HashCode.Combine(robot.Coordinates.GetHashCode(), robot.Direction.GetHashCode());
                            if (badPoints.Contains(movingHashCode))
                                continue;

                            var coordinatesNew = ReturnNewCoordinates(robot.Coordinates, robot.Direction, command.Value);
                            if (Map.IsCoordinatesOutOfMap(coordinatesNew))
                            {
                                badPoints.Add(movingHashCode);
                                robot.SetIsLostToTrue();
                            }
                            else
                                robot.SetCoordinates(coordinatesNew);
                            break;

                        default:
                            throw new ArgumentException($"wrong type of command - {command.Type}");
                    }
                }
            }
        }

        private Direction ReturnNewDirection(Direction direction, Command command)
        {
            return command switch
            {
                Command.Left => ReturnDirectionAfterTurnLeft(direction),
                Command.Right => ReturnDirectionAfterTurnRight(direction),
                _ => throw new ArgumentException($"wrong turning command - {command}")
            };           
        }

        private Direction ReturnDirectionAfterTurnLeft(Direction robotDirectionCurrent)
        {
            var robotDirectionNew = robotDirectionCurrent - 1;

            if ((int)robotDirectionNew < 0)
                robotDirectionNew = Enum.GetValues(typeof(Direction)).Cast<Direction>().Last();

            return robotDirectionNew;
        }

        private Direction ReturnDirectionAfterTurnRight(Direction robotDirectionCurrent)
        {
            var robotDirectionNew = robotDirectionCurrent + 1;
            var robotDirectionMax = Enum.GetNames(typeof(Direction)).Length;

            if ((int)robotDirectionNew >= robotDirectionMax)
                robotDirectionNew = Enum.GetValues(typeof(Direction)).Cast<Direction>().First();

            return robotDirectionNew;
        }

        private Coordinates ReturnNewCoordinates(Coordinates coordinates, Direction direction, Command command)
        {
            return command switch
            {
                Command.Forward => ReturnNewCoordinates(coordinates, direction),
                _ => throw new ArgumentException($"wrong moving command - {command}"),
            };
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
                    throw new ArgumentException($"wrong direction - {direction}");

            }
            return new Coordinates(coordinateNewX, coordinateNewY);
        }
    }
}
