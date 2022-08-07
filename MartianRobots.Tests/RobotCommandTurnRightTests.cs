using MartianRobots.Commands;
using MartianRobots.Model;

namespace MartianRobots.Tests
{
    public class RobotCommandTurnRightTests
    {
        [Theory]
        [InlineData(Orientation.North, Orientation.East)]
        [InlineData(Orientation.East, Orientation.South)]
        [InlineData(Orientation.South, Orientation.West)]
        [InlineData(Orientation.West, Orientation.North)]
        public void Execute_CorrectTurn_Success(Orientation orientationSrc, Orientation orientationDest)
        {
            // Arrange
            var robotId = 0;
            var coordinates = new Coordinates(1, 2);
            var robot = new Robot(robotId, coordinates, orientationSrc);
            var commandTurnRight = RobotCommandTurnRight.GetInstance();
            var map = new Map(33, 33);

            // Act
            commandTurnRight.Execute(robot, map);

            // Assert
            Assert.Equal(orientationDest, robot.Orientation);
        }

        [Theory]
        [InlineData(Orientation.South, Orientation.East)]
        [InlineData(Orientation.East, Orientation.North)]
        [InlineData(Orientation.North, Orientation.West)]
        [InlineData(Orientation.West, Orientation.South)]
        public void Execute_WrongTurn_Succes(Orientation orientationSrc, Orientation orientationDest)
        {
            // Arrange
            var robotId = 0;
            var coordinates = new Coordinates(1, 2);
            var robot = new Robot(robotId, coordinates, orientationSrc);
            var commandTurnRight = RobotCommandTurnRight.GetInstance();
            var map = new Map(33, 33);

            // Act
            commandTurnRight.Execute(robot, map);

            // Assert
            Assert.NotEqual(orientationDest, robot.Orientation);
        }
    }
}
