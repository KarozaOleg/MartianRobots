using MartianRobots.Commands;
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
                new RobotCommands(0, new List<IRobotCommand>()
                { 
                    //RFRFRFRF
                    RobotCommandTurnRight.GetInstance(),
                    RobotCommandMoveForward.GetInstance(),
                    RobotCommandTurnRight.GetInstance(),
                    RobotCommandMoveForward.GetInstance(),
                    RobotCommandTurnRight.GetInstance(),
                    RobotCommandMoveForward.GetInstance(),
                    RobotCommandTurnRight.GetInstance(),
                    RobotCommandMoveForward.GetInstance()
                }),
                new RobotCommands(1, new List<IRobotCommand>()
                {
                    //FRRFLLFFRRFLL
                    RobotCommandMoveForward.GetInstance(),
                    RobotCommandTurnRight.GetInstance(),
                    RobotCommandTurnRight.GetInstance(),
                    RobotCommandMoveForward.GetInstance(),
                    RobotCommandTurnLeft.GetInstance(),
                    RobotCommandTurnLeft.GetInstance(),
                    RobotCommandMoveForward.GetInstance(),
                    RobotCommandMoveForward.GetInstance(),
                    RobotCommandTurnRight.GetInstance(),
                    RobotCommandTurnRight.GetInstance(),
                    RobotCommandMoveForward.GetInstance(),
                    RobotCommandTurnLeft.GetInstance(),
                    RobotCommandTurnLeft.GetInstance()
                }),
                new RobotCommands(2, new List<IRobotCommand>()
                {
                    //LL,FFF,LF,LF,L
                    RobotCommandTurnLeft.GetInstance(),
                    RobotCommandTurnLeft.GetInstance(),
                    RobotCommandMoveForward.GetInstance(),
                    RobotCommandMoveForward.GetInstance(),
                    RobotCommandMoveForward.GetInstance(),
                    RobotCommandTurnLeft.GetInstance(),
                    RobotCommandMoveForward.GetInstance(),
                    RobotCommandTurnLeft.GetInstance(),
                    RobotCommandMoveForward.GetInstance(),
                    RobotCommandTurnLeft.GetInstance()
                })
            };
            return new InputData(5, 3, robots, robotsCommands);
        }
    }
}
