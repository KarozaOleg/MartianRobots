using MartianRobots.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MartianRobots.Service
{
    public class CommandExecutionService
    {
        private Map Map { get; }
        private HashSet<int> DropOffHashCodes { get; }

        public CommandExecutionService(Map map)
        {
            Map = map ?? throw new ArgumentException(nameof(map));
            DropOffHashCodes = new HashSet<int>();
        }

        public void ExecuteCommand(Robot robot, Command command)
        {
            if (robot.IsLost)
                return;
            
            switch (ReturnCommandType(command))
            {
                case CommandType.Turning:
                    var directionNew = ReturnNewDirectionAfterTurning(robot.Direction, command);
                    robot.SetDirection(directionNew);
                    break;

                case CommandType.Moving:
                    var movingHashCode = HashCode.Combine(robot.Coordinates.GetHashCode(), robot.Direction.GetHashCode());
                    if (DropOffHashCodes.Contains(movingHashCode))
                        return;

                    var coordinatesNew = ReturnNewCoordinatesAfterMoving(robot.Coordinates, robot.Direction, command);
                    if (Map.IsCoordinatesOutOfMap(coordinatesNew))
                    {
                        DropOffHashCodes.Add(movingHashCode);
                        robot.SetIsLostMarkToTrue();
                    }
                    else
                        robot.SetCoordinates(coordinatesNew);
                    break;

                default:
                    throw new ArgumentException($"wrong type of command - {command}");
            }        
        }

        protected virtual CommandType ReturnCommandType(Command command)
        {
            return command switch
            {
                Command.Left => CommandType.Turning,
                Command.Right => CommandType.Turning,
                Command.Forward => CommandType.Moving,
                _ => throw new ArgumentException($"unknown command:{command}")
            };
        }

        protected virtual Direction ReturnNewDirectionAfterTurning(Direction direction, Command command)
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

        protected virtual Coordinates ReturnNewCoordinatesAfterMoving(Coordinates coordinates, Direction direction, Command command)
        {
            return command switch
            {
                Command.Forward => ReturnNewCoordinatesAfterMovingForward(coordinates, direction),
                _ => throw new ArgumentException($"wrong moving command - {command}"),
            };
        }

        private Coordinates ReturnNewCoordinatesAfterMovingForward(Coordinates coordinatesCurrent, Direction direction)
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
