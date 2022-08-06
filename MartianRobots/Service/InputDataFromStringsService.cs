using MartianRobots.Interface;
using MartianRobots.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MartianRobots.Service
{
    internal class InputDataFromStringsService : IInputDataService
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

        protected virtual InputData Parse()
        {
            var mapWidth = default(int?);
            var mapHeight = default(int?);
            var robots = new List<Robot>();
            var robotsCommands = new List<RobotCommand>();

            var robotId = 0;
            for (int i = 0; i < InputDataStrings.Length; i++)
            {
                var lineSplitted = InputDataStrings[i].Split(' ');
                //map size
                if (lineSplitted.Length == 2)
                {
                    if (int.TryParse(lineSplitted[0], out var mapWidthParsed) == false)
                        throw new InvalidCastException($"cast map width:{lineSplitted[0]} into integer");
                    mapWidth = mapWidthParsed;
                    if (int.TryParse(lineSplitted[1], out var mapHeightParsed) == false)
                        throw new InvalidCastException($"cast map height:{lineSplitted[1]} into integer");
                    mapHeight = mapHeightParsed;
                }
                //robot
                else if (lineSplitted.Length == 3)
                {
                    if (int.TryParse(lineSplitted[0], out var x) == false)
                        throw new InvalidCastException($"cast robot x coordinate:{lineSplitted[0]} into integer");
                    if (int.TryParse(lineSplitted[1], out var y) == false)
                        throw new InvalidCastException($"cast robot y coordinate:{lineSplitted[1]} into integer");
                    if (ParseEnum(lineSplitted[2], out Direction direction) == false)
                        throw new InvalidCastException($"cast robot direction:{lineSplitted[2]} into enum item");

                    var robot = new Robot(robotId, new Coordinates(x, y), direction);
                    robots.Add(robot);
                }
                //commands
                else if (lineSplitted.Length == 1)
                {
                    var commands = lineSplitted[0]
                        .ToCharArray()
                        .Select(c => ParseCommand(c))
                        .ToList();
                    var robotCommands = new RobotCommand(robotId++, commands);
                    robotsCommands.Add(robotCommands);
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

        private Command ParseCommand(char c)
        {
            return c switch
            {
                'L' => Command.Left,
                'R' => Command.Right,
                'F' => Command.Forward,
                _ => throw new ArgumentException($"unknow command char:{c}")
            };
        }
        
        private bool ParseEnum<T>(string str, out T value)
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
