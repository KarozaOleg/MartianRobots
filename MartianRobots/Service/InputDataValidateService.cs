using MartianRobots.Model;
using System;
using System.Collections.Generic;

namespace MartianRobots.Service
{
    /// <summary>
    /// Represents validation methods for application input data
    /// </summary>
    public class InputDataValidateService
    {
        private static int CoordinateValueMax { get; }
        private static int RobotCommandsAmoutMax { get; }

        static InputDataValidateService()
        {
            CoordinateValueMax = 50;
            RobotCommandsAmoutMax = 100;
        }

        /// <summary>
        /// Validate application input data
        /// </summary>
        /// <param name="inputData"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void Validate(InputData inputData)
        {
            if (inputData == null)
                throw new ArgumentNullException(nameof(inputData));

            ValidateMapSize(inputData.MapWidth, inputData.MapHeight, CoordinateValueMax);

            foreach (var robot in inputData.Robots)            
                ValidateRobot(robot, CoordinateValueMax);

            ValidateCommands(inputData.RobotsCommands, RobotCommandsAmoutMax);
        }

        /// <summary>
        /// Validate input map size
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void ValidateMapSize(int mapWidth, int mapHeight, int coordinateValueMax)
        {
            if (mapWidth < 1)
                throw new ArgumentOutOfRangeException($"{nameof(mapWidth)} less than 1");
            if (mapHeight < 1)
                throw new ArgumentOutOfRangeException($"{nameof(mapHeight)} less than 1");
            if (mapWidth > coordinateValueMax)
                throw new ArgumentOutOfRangeException(nameof(mapWidth));
            if (mapHeight > coordinateValueMax)
                throw new ArgumentOutOfRangeException(nameof(mapHeight));
        }

        public static void ValidateRobot(Robot robot, int coordinateValueMax)
        {
            if (robot.Coordinates.X > coordinateValueMax)
                throw new ArgumentOutOfRangeException(nameof(robot.Coordinates.X));
            if (robot.Coordinates.Y > coordinateValueMax)
                throw new ArgumentOutOfRangeException(nameof(robot.Coordinates.Y));
        }

        public static void ValidateCommands(List<RobotCommands> robotsCommands, int robotCommandsAmountMax)
        {
            foreach (var robotCommands in robotsCommands)
                if (robotCommands.Commands.Count >= robotCommandsAmountMax)
                    throw new ArgumentOutOfRangeException("amount of robot commands more than max value");
        }
    }
}
