using MartianRobots.Model;
using MartianRobots.Service;

namespace MartianRobots.Tests
{
    public class OutputServiceTests
    {
        [Theory]
        [InlineData(1, 1, Orientation.North, true, "1 1 N LOST")]
        [InlineData(2, 5, Orientation.West, false, "2 5 W")]
        [InlineData(13, 6, Orientation.South, true, "13 6 S LOST")]
        public void GetRobotStatus_CheckEquals_Equal(int x, int y, Orientation orientation, bool isLost, string outputStrExcpected)
        {
            // Arrange
            var coordinates = new Coordinates(x, y);
            var robot = new Robot(0, coordinates, orientation);
            if (isLost)
                robot.SetIsLostMarkToTrue();

            // Act
            var outputStr = OutputService.GetRobotStatus(robot);

            // Assert
            Assert.Equal(outputStrExcpected, outputStr);
        }

        [Fact]
        public void GetRobotStatus_PassNull_ThrowException()
        {
            // Act
            Action act = () => OutputService.GetRobotStatus(null);

            // Assert
            Assert.Throws<ArgumentNullException>(act);
        }
    }
}
