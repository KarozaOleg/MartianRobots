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
                new Robot(0, new Coordinates(1, 1), Direction.East),
                new Robot(1, new Coordinates(3, 2), Direction.North),
                new Robot(2, new Coordinates(0, 3), Direction.West)
            };
            var robotsCommands = new List<RobotCommand>()
            {
                new RobotCommand(0, new List<(CommandType type, Command command)>()
                { 
                    //RF,RF,RF,RF
                    new (CommandType.Turning, Command.Right),
                    new (CommandType.Moving, Command.Forward),
                    new (CommandType.Turning, Command.Right),
                    new (CommandType.Moving, Command.Forward),
                    new (CommandType.Turning, Command.Right),
                    new (CommandType.Moving, Command.Forward),
                    new (CommandType.Turning, Command.Right),
                    new (CommandType.Moving, Command.Forward)
                }),
                new RobotCommand(1, new List<(CommandType type, Command command)>()
                {
                    //FRRFLLFFRRFLL
                    new (CommandType.Moving, Command.Forward),
                    new (CommandType.Turning, Command.Right),
                    new (CommandType.Turning, Command.Right),
                    new (CommandType.Moving, Command.Forward),
                    new (CommandType.Turning, Command.Left),
                    new (CommandType.Turning, Command.Left),
                    new (CommandType.Moving, Command.Forward),
                    new (CommandType.Moving, Command.Forward),
                    new (CommandType.Turning, Command.Right),
                    new (CommandType.Turning, Command.Right),
                    new (CommandType.Moving, Command.Forward),
                    new (CommandType.Turning, Command.Left),
                    new (CommandType.Turning, Command.Left)
                }),
                new RobotCommand(2, new List<(CommandType type, Command command)>()
                {
                    //LL,FFF,LF,LF,L
                    new (CommandType.Turning, Command.Left),
                    new (CommandType.Turning, Command.Left),

                    new (CommandType.Moving, Command.Forward),
                    new (CommandType.Moving, Command.Forward),
                    new (CommandType.Moving, Command.Forward),

                    new (CommandType.Turning, Command.Left),
                    new (CommandType.Moving, Command.Forward),

                    new (CommandType.Turning, Command.Left),
                    new (CommandType.Moving, Command.Forward),

                    new (CommandType.Turning, Command.Left)
                })
            };
            return new InputData(5, 3, robots, robotsCommands);
        }
    }
}
