using MartianRobots.Commands;
using MartianRobots.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MartianRobots.Tests
{
    public class RobotCommandTurnLeftTests
    {
        [Theory]
        [InlineData(Orientation.South, Orientation.East)]
        [InlineData(Orientation.East, Orientation.North)]
        [InlineData(Orientation.North, Orientation.West)]
        [InlineData(Orientation.West, Orientation.South)]
        public void Execute_CorrectTurn_Success(Orientation orientationSrc, Orientation orientationDest)
        {
            // Arrange
            var robotId = 0;
            var coordinates = new Coordinates(1, 2);
            var robot = new Robot(robotId, coordinates, orientationSrc);
            var commandTurnLeft = RobotCommandTurnLeft.GetInstance();
            var map = new Map(33, 33);

            // Act
            commandTurnLeft.Execute(robot, map);

            // Assert
            Assert.Equal(orientationDest, robot.Orientation);
        }

        [Theory]
        [InlineData(Orientation.South, Orientation.West)]
        [InlineData(Orientation.East, Orientation.East)]
        [InlineData(Orientation.North, Orientation.North)]
        [InlineData(Orientation.West, Orientation.East)]
        public void Execute_WrongTurn_Succes(Orientation orientationSrc, Orientation orientationDest)
        {
            // Arrange
            var robotId = 0;
            var coordinates = new Coordinates(1, 2);
            var robot = new Robot(robotId, coordinates, orientationSrc);
            var commandTurnLeft = RobotCommandTurnLeft.GetInstance();
            var map = new Map(33, 33);

            // Act
            commandTurnLeft.Execute(robot, map);

            // Assert
            Assert.NotEqual(orientationDest, robot.Orientation);
        }
    }
}
