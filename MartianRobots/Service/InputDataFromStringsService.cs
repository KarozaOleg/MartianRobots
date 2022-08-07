using MartianRobots.Commands;
using MartianRobots.Interface;
using MartianRobots.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MartianRobots.Service
{
    /// <summary>
    /// Implements parsing algorithm input data strings
    /// </summary>
    public class InputDataFromStringsService : IInputDataService
    {
        private string[] InputDataStrings { get; }
        private InputData InputData;

        public InputDataFromStringsService(string[] inputDataStrings)
        {
            InputDataStrings = inputDataStrings ?? throw new ArgumentNullException(nameof(inputDataStrings));
        }

        public InputData GetInputData()
        {
            if (InputData == null)
                InputData = Parse();
            
            return InputData;
        }

        public InputData Parse()
        {
            var mapWidth = default(int?);
            var mapHeight = default(int?);
            var robots = new List<Robot>();
            var robotsCommands = new List<RobotCommands>();

            var robotId = 0;
            for (int i = 0; i < InputDataStrings.Length; i++)
            {
                var line = InputDataStrings[i];
                var amountOfSpaces = line.Count(c => c == ' ');
                if (amountOfSpaces == 1)
                {
                    ParseMapSize(line, out mapWidth, out mapHeight);
                }
                else if (amountOfSpaces == 2)
                {
                    var robot = ParseRobot(line, robotId);
                    robots.Add(robot);
                }
                else if (amountOfSpaces == 0)
                {
                    var robotCommands = ParseRobotCommands(line, robotId);
                    robotsCommands.Add(robotCommands);
                    robotId++;
                }
                else
                    throw new ArgumentException($"wrong pattern of input string:{InputDataStrings[i]}");
            }

            if (mapWidth.HasValue == false)
                throw new ArgumentNullException(nameof(mapWidth));
            if (mapHeight.HasValue == false)
                throw new ArgumentNullException(nameof(mapHeight));

            return new InputData(mapWidth.Value, mapHeight.Value, robots, robotsCommands);
        }

        public static void ParseMapSize(string line, out int? width, out int? height)
        {
            var lineSplitted = line.Split(' ');

            if (int.TryParse(lineSplitted[0], out var widthParsed) == false)
                throw new InvalidCastException($"cast map width:{lineSplitted[0]} into integer");
            width = widthParsed;
            if (int.TryParse(lineSplitted[1], out var heightParsed) == false)
                throw new InvalidCastException($"cast map height:{lineSplitted[1]} into integer");
            height = heightParsed;
        }

        public static Robot ParseRobot(string line, int robotId)
        {
            var lineSplitted = line.Split(' ');

            if (int.TryParse(lineSplitted[0], out var x) == false)
                throw new InvalidCastException($"cast robot x coordinate:{lineSplitted[0]} into integer");
            if (int.TryParse(lineSplitted[1], out var y) == false)
                throw new InvalidCastException($"cast robot y coordinate:{lineSplitted[1]} into integer");
            if (ParseEnum(lineSplitted[2], out Orientation orientation) == false)
                throw new InvalidCastException($"cast robot orientation:{lineSplitted[2]} into enum item");

            return new Robot(robotId, new Coordinates(x, y), orientation);
        }

        public static RobotCommands ParseRobotCommands(string line, int robotId)
        {
            var commands = line
                       .ToCharArray()
                       .Select(c => ParseCommand(c))
                       .ToList();
            return new RobotCommands(robotId, commands);
        }

        public static IRobotCommand ParseCommand(char c)
        {
            return c switch
            {
                'L' => RobotCommandTurnLeft.GetInstance(),
                'R' => RobotCommandTurnRight.GetInstance(),
                'F' => RobotCommandMoveForward.GetInstance(),
                _ => throw new ArgumentException($"unknow command:{c}")
            };
        }
        
        private static bool ParseEnum<T>(string str, out T value)
        {
            value = default(T);
            foreach (T item in (T[])Enum.GetValues(typeof(T)))
            {
                if (item.ToString().ToLower().First() != str.ToLower().First())
                    continue;

                value = item;
                return true;
            }
            return false;
        }
    }
}
