using MartianRobots.Model;
using MartianRobots.Service;

namespace MartianRobots.Tests
{
    public class InputDataValidateServiceTests
    {
        [Theory]
        [InlineData(51, 3, 50)]
        [InlineData(1, 66, 50)]
        [InlineData(0, 7, 50)]
        [InlineData(8, 0, 50)]
        [InlineData(-1, 5, 50)]
        [InlineData(4, -4, 50)]
        public void ValidateMapSize_WrongInput_ThrowException (int width, int height, int coordinateMaxValue)
        {
            // Act
            Action act = () => InputDataValidateService.ValidateMapSize(width, height, coordinateMaxValue);

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(act);
        }

        [Theory]
        [InlineData(51, 3, 50)]
        [InlineData(1, 66, 50)]
        public void ValidateRobot_WrongInput_ThrowException(int x, int y, int coordinateMaxValue)
        {
            // Assert
            var robotId = 0;
            var coordinates = new Coordinates(x, y);
            var robot = new Robot(robotId, coordinates, Orientation.North);

            // Act
            Action act = () => InputDataValidateService.ValidateRobot(robot, coordinateMaxValue);

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(act);
        }

        [Fact]
        public void ValidateCommands_WrongInput_ThrowException()
        {
            // Assert
            var robotCommandsAmountMax = 100;
            var commandsStr = new string('F', robotCommandsAmountMax);
            var robotCommands = InputDataFromStringsService.ParseRobotCommands(commandsStr, 0);

            var commands = new List<RobotCommands>()
            {
                robotCommands
            };

            // Act
            Action act = () => InputDataValidateService.ValidateCommands(commands, robotCommandsAmountMax);

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(act);
        }

        [Fact]
        public void Validate_NullInput_ThrowException()
        {
            // Act
            Action act = () => InputDataValidateService.Validate(null);

            // Assert
            Assert.Throws<ArgumentNullException>(act);
        }
    }
}
