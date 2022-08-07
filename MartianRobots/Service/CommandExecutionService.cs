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

        /// <summary>
        /// Implemets logic for execute command for robot
        /// </summary>
        /// <param name="map"></param>
        /// <exception cref="ArgumentException"></exception>
        public CommandExecutionService(Map map)
        {
            Map = map ?? throw new ArgumentException(nameof(map));
            DropOffHashCodes = new HashSet<int>();
        }

        /// <summary>
        /// Execute one command for robot
        /// </summary>
        /// <param name="robot"></param>
        /// <param name="command"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public void ExecuteCommand(Robot robot, Command command)
        {
            if (robot.IsLost)
                return;
            
            switch (ReturnCommandType(command))
            {
                case CommandType.Turning:
                    var orientation = ReturnOrientationAfterTurning(robot.Orientation, command);
                    robot.SetOrientation(orientation);
                    break;

                case CommandType.Moving:
                    var movingHashCode = HashCode.Combine(robot.Coordinates.GetHashCode(), robot.Orientation.GetHashCode());
                    if (DropOffHashCodes.Contains(movingHashCode))
                        return;

                    var coordinates = ReturnCoordinatesAfterMoving(robot.Coordinates, robot.Orientation, command);
                    if (Map.IsCoordinatesOutOfMap(coordinates))
                    {
                        DropOffHashCodes.Add(movingHashCode);
                        robot.SetIsLostMarkToTrue();
                    }
                    else
                        robot.SetCoordinates(coordinates);
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

        protected virtual Orientation ReturnOrientationAfterTurning(Orientation orientation, Command command)
        {
            return command switch
            {
                Command.Left => ReturnOrientationAfterTurnLeft(orientation),
                Command.Right => ReturnOrientationAfterTurnRight(orientation),
                _ => throw new ArgumentException($"wrong turning command - {command}")
            };           
        }

        private Orientation ReturnOrientationAfterTurnLeft(Orientation orientationCurrent)
        {
            var orientation = orientationCurrent - 1;

            if ((int)orientation < 0)
                orientation = Enum.GetValues(typeof(Orientation)).Cast<Orientation>().Last();

            return orientation;
        }

        private Orientation ReturnOrientationAfterTurnRight(Orientation orientationCurrent)
        {
            var orientation = orientationCurrent + 1;
            var orientationMax = Enum.GetNames(typeof(Orientation)).Length;

            if ((int)orientation >= orientationMax)
                orientation = Enum.GetValues(typeof(Orientation)).Cast<Orientation>().First();

            return orientation;
        }

        protected virtual Coordinates ReturnCoordinatesAfterMoving(Coordinates coordinates, Orientation orientation, Command command)
        {
            return command switch
            {
                Command.Forward => ReturnCoordinatesAfterMovingForward(coordinates, orientation),
                _ => throw new ArgumentException($"wrong moving command - {command}"),
            };
        }

        private Coordinates ReturnCoordinatesAfterMovingForward(Coordinates coordinatesCurrent, Orientation orientation)
        {
            var coordinateNewX = coordinatesCurrent.X;
            var coordinateNewY = coordinatesCurrent.Y;
            switch (orientation)
            {
                case Orientation.North:
                    coordinateNewY = coordinatesCurrent.Y + 1;
                    break;

                case Orientation.East:
                    coordinateNewX = coordinatesCurrent.X + 1;
                    break;

                case Orientation.South:
                    coordinateNewY = coordinatesCurrent.Y - 1;
                    break;

                case Orientation.West:
                    coordinateNewX = coordinatesCurrent.X - 1;
                    break;

                default:
                    throw new ArgumentException($"wrong orientation - {orientation}");

            }
            return new Coordinates(coordinateNewX, coordinateNewY);
        }
    }
}
