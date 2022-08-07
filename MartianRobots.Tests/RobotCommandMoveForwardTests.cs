using MartianRobots.Commands;
using MartianRobots.Model;

namespace MartianRobots.Tests
{
    public class RobotCommandMoveForwardTests
    {
        [Theory]
        [InlineData(Orientation.North, 5, 5, 5, 6)]
        [InlineData(Orientation.East, 5, 5, 6, 5)]
        [InlineData(Orientation.South, 5, 5, 5, 4)]
        [InlineData(Orientation.West, 5, 5, 4, 5)]
        public void Execute_CorrectMove_Success(Orientation orientation, int xSrc, int ySrc, int xDest, int yDest)
        {
            // Arrange
            var robotId = 0;
            var coordinates = new Coordinates(xSrc, ySrc);
            var robot = new Robot(robotId, coordinates, orientation);
            var commandMoveForward = RobotCommandMoveForward.GetInstance();
            var map = new Map(33, 33);

            // Act
            commandMoveForward.Execute(robot, map);

            // Assert
            var coordinatesDest = new Coordinates(xDest, yDest);
            Assert.Equal(coordinatesDest, robot.Coordinates);
        }

        [Theory]
        [InlineData(Orientation.North, 5, 5, 1, 1)]
        [InlineData(Orientation.East, 5, 5, 3, 43)]
        [InlineData(Orientation.South, 5, 5, 2, 7)]
        [InlineData(Orientation.West, 5, 5, 13, 4)]
        public void Execute_WrongMove_Success(Orientation orientation, int xSrc, int ySrc, int xDest, int yDest)
        {
            // Arrange
            var robotId = 0;
            var coordinates = new Coordinates(xSrc, ySrc);
            var robot = new Robot(robotId, coordinates, orientation);
            var commandMoveForward = RobotCommandMoveForward.GetInstance();
            var map = new Map(33, 33);

            // Act
            commandMoveForward.Execute(robot, map);

            // Assert
            var coordinatesDest = new Coordinates(xDest, yDest);
            Assert.NotEqual(coordinatesDest, robot.Coordinates);
        }
                
        [Theory]
        [InlineData(Orientation.North, 1, 5)]
        [InlineData(Orientation.East, 5, 1)]
        [InlineData(Orientation.South, 1, 0)]
        [InlineData(Orientation.West, 0, 1)]
        public void Execute_DropOff_IsLostEqualTrue(Orientation orientation, int x, int y)
        {
            // Arrange
            var robotId = 0;
            var coordinates = new Coordinates(x, y);
            var robot = new Robot(robotId, coordinates, orientation);
            var commandMoveForward = RobotCommandMoveForward.GetInstance();
            var map = new Map(5, 5);

            // Act
            commandMoveForward.Execute(robot, map);

            // Assert
            Assert.True(robot.IsLost);
        }
    }
}
