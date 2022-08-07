using MartianRobots.Commands;
using MartianRobots.Model;
using MartianRobots.Service;
using Newtonsoft.Json;

namespace MartianRobots.Tests
{
    public class InputDataFromStringsServiceTests
    {
        [Theory]
        [InlineData("1 2", 1, 2)]
        [InlineData("0 0", 0, 0)]
        [InlineData("7 7", 7, 7)]
        [InlineData("2 23", 2, 23)]
        public void ParseMapSize_FromLine_Equal(string line, int width, int height)
        {
            // Arrange
            InputDataFromStringsService.ParseMapSize(line, out var widthParsed, out var heightParsed);

            // Assert
            Assert.Equal(width, widthParsed);
            Assert.Equal(height, heightParsed);
        }

        [Theory]
        [InlineData("1 1 E", 1, 1, Orientation.East)]
        [InlineData("3 2 N", 3, 2, Orientation.North)]
        [InlineData("0 3 W", 0, 3, Orientation.West)]
        [InlineData("1 7 S", 1, 7, Orientation.South)]
        [InlineData("0 0 W", 0, 0, Orientation.West)]
        public void Robot_FromLine_Equal(string line, int x, int y, Orientation orientation)
        {
            // Arrange
            var robot = InputDataFromStringsService.ParseRobot(line, 0);

            // Assert
            Assert.Equal(new Coordinates(x, y), robot.Coordinates);
            Assert.Equal(orientation, robot.Orientation);
            Assert.False(robot.IsLost);
        }

        [Theory]
        [InlineData('L', typeof(RobotCommandTurnLeft))]
        [InlineData('R', typeof(RobotCommandTurnRight))]
        [InlineData('F', typeof(RobotCommandMoveForward))]
        public void ParseRobotCommand_FromChar_Equal(char c, Type type)
        {
            // Arrange
            var robotCommand = InputDataFromStringsService.ParseCommand(c);

            // Assert
            Assert.Equal(type, robotCommand.GetType());
        }

        [Theory]
        [InlineData("LRF", 0, typeof(RobotCommandTurnLeft), typeof(RobotCommandTurnRight), typeof(RobotCommandMoveForward))]
        [InlineData("LLL", 0, typeof(RobotCommandTurnLeft), typeof(RobotCommandTurnLeft), typeof(RobotCommandTurnLeft))]
        [InlineData("RRR", 0, typeof(RobotCommandTurnRight), typeof(RobotCommandTurnRight), typeof(RobotCommandTurnRight))]
        [InlineData("FFF", 0, typeof(RobotCommandMoveForward), typeof(RobotCommandMoveForward), typeof(RobotCommandMoveForward))]
        public void ParseRobotCommand_FromLine_Equals(string line, int robotId, Type typeLeft, Type typeRight, Type typeForward)
        {
            // Arrange
            var robotCommands = InputDataFromStringsService.ParseRobotCommands(line, 0);

            // Assert
            Assert.StrictEqual(robotCommands.Id, robotId);
            Assert.Collection(robotCommands.Commands,
                item => Assert.Equal(typeLeft, item.GetType()),
                item => Assert.Equal(typeRight, item.GetType()),
                item => Assert.Equal(typeForward, item.GetType())
            );
        }

        [Fact]
        public void ParseInputData_FromLines_Equal()
        {
            // Arrange
            var inputStrings = new string[]
            {
                "5 3",
                "1 1 E",
                "RFRFRFRF",
                "3 2 N",
                "FRRFLLFFRRFLL",
                "0 3 W",
                "LLFFFLFLFL"
            };
            var InputDataFromStringsService = new InputDataFromStringsService(inputStrings);

            // Act
            var inputData = InputDataFromStringsService.GetInputData();

            // Assert
            Assert.Equal(5, inputData.MapWidth);
            Assert.Equal(3, inputData.MapHeight);

            var robot1 = JsonConvert.SerializeObject(new Robot(0, new Coordinates(1, 1), Orientation.East));
            var robot1Parsed = JsonConvert.SerializeObject(inputData.Robots[0]);
            Assert.Equal(robot1, robot1Parsed);

            var robot2 = JsonConvert.SerializeObject(new Robot(1, new Coordinates(3, 2), Orientation.North));
            var robot2Parsed = JsonConvert.SerializeObject(inputData.Robots[1]);
            Assert.Equal(robot2, robot2Parsed);

            var robot3 = JsonConvert.SerializeObject(new Robot(2, new Coordinates(0, 3), Orientation.West));
            var robot3Parsed = JsonConvert.SerializeObject(inputData.Robots[2]);
            Assert.Equal(robot3, robot3Parsed);
        }
    }
}