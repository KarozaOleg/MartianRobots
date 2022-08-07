using MartianRobots.Model;
using System;
using System.Collections.Generic;

namespace MartianRobots.Service
{
    public class InputDataValidateService
    {
        protected virtual int CoordinateValueMax { get; }
        protected virtual int RobotCommandsAmoutMax { get; }

        /// <summary>
        /// Represents validation methods for application input data
        /// </summary>
        public InputDataValidateService()
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
        public void Validate(InputData inputData)
        {
            if (inputData == null)
                throw new ArgumentNullException(nameof(inputData));

            ValidateMapSize(inputData.MapWidth, inputData.MapHeight);

            foreach (var robot in inputData.Robots)            
                ValidateRobot(robot);

            ValidateCommands(inputData.RobotsCommands);
        }

        protected virtual void ValidateMapSize(int mapWidth, int mapHeight)
        {
            if (mapWidth < 1)
                throw new ArgumentOutOfRangeException($"{nameof(mapWidth)} less than 1");
            if (mapHeight < 1)
                throw new ArgumentOutOfRangeException($"{nameof(mapHeight)} less than 1");
            if (mapWidth > CoordinateValueMax)
                throw new ArgumentOutOfRangeException(nameof(mapWidth));
            if (mapHeight > CoordinateValueMax)
                throw new ArgumentOutOfRangeException(nameof(mapHeight));
        }

        protected virtual void ValidateRobot(Robot robot)
        {
            if (robot.Coordinates.X > CoordinateValueMax)
                throw new ArgumentOutOfRangeException(nameof(robot.Coordinates.X));
            if (robot.Coordinates.Y > CoordinateValueMax)
                throw new ArgumentOutOfRangeException(nameof(robot.Coordinates.Y));
        }

        protected virtual void ValidateCommands(List<RobotCommands> robotsCommands)
        {
            foreach (var robotCommands in robotsCommands)
                if (robotCommands.Commands.Count >= RobotCommandsAmoutMax)
                    throw new ArgumentOutOfRangeException("amount of robot commands more than max value");
        }
    }
}
