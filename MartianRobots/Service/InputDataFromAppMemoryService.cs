using MartianRobots.Interface;
using MartianRobots.Model;
using System.Collections.Generic;

namespace MartianRobots.Service
{
    internal class InputDataFromAppMemoryService : IInputDataService
    {
        public InputData GetInputData()
        {
            var robots = new List<Robot>()
            {
                new Robot(0, new Coordinates(1, 1), Orientation.East),
                new Robot(1, new Coordinates(3, 2), Orientation.North),
                new Robot(2, new Coordinates(0, 3), Orientation.West)
            };
            var robotsCommands = new List<RobotCommands>()
            {
                new RobotCommands(0, new List<Command>()
                { 
                    //RFRFRFRF
                    Command.Right,
                    Command.Forward,
                    Command.Right,
                    Command.Forward,
                    Command.Right,
                    Command.Forward,
                    Command.Right,
                    Command.Forward
                }),
                new RobotCommands(1, new List<Command>()
                {
                    //FRRFLLFFRRFLL
                    Command.Forward,
                    Command.Right,
                    Command.Right,
                    Command.Forward,
                    Command.Left,
                    Command.Left,
                    Command.Forward,
                    Command.Forward,
                    Command.Right,
                    Command.Right,
                    Command.Forward,
                    Command.Left,
                    Command.Left
                }),
                new RobotCommands(2, new List<Command>()
                {
                    //LL,FFF,LF,LF,L
                    Command.Left,
                    Command.Left,
                    Command.Forward,
                    Command.Forward,
                    Command.Forward,
                    Command.Left,
                    Command.Forward,
                    Command.Left,
                    Command.Forward,
                    Command.Left
                })
            };
            return new InputData(5, 3, robots, robotsCommands);
        }
    }
}
