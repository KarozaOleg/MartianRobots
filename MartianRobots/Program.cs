using MartianRobots.Model;
using MartianRobots.Repository;
using MartianRobots.Service;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MartianRobots
{
    class Program
    {
        private static ILogger Logger { get; }
        private static List<Robot> Robots { get; }
        private static Dictionary<int, List<Command>> Commands { get; }
        private static CommandExecutionService CommandExecutionService { get; }

        static Program()
        {
            try
            {
                Logger = LogManager.GetCurrentClassLogger();

                var inputDataRepository = new InputDataStringsRepository();
                var inputDataService = new InputDataFromStringsService(inputDataRepository.GetInputData());
                var inputData = inputDataService.GetInputData();

                var inputDataValidateService = new InputDataValidateService();
                inputDataValidateService.Validate(inputData);

                Robots = inputData.Robots;
                Commands = inputData.RobotsCommands.ToDictionary(c => c.Id, c => c.Commands);

                var map = new Map(inputData.MapWidth, inputData.MapHeight);
                CommandExecutionService = new CommandExecutionService(map);
            }
            catch(Exception ex)
            {
                Logger?.Error(ex, ".ctor problem");
                throw;
            }
        }

        static void Main()
        {
            try
            {
                foreach (var robot in Robots)
                {
                    if (Commands.ContainsKey(robot.Id) == false)
                        continue;

                    foreach (var command in Commands[robot.Id])
                        CommandExecutionService.ExecuteCommand(robot, command);
                }

                foreach (var robot in Robots)
                    Console.WriteLine(ReturnRobotStatus(robot));
            }
            catch(Exception ex)
            {
                Logger?.Error(ex, "main problem");
            }
        }

        static string ReturnRobotStatus(Robot robot)
        {
            return $"{robot.Coordinates} {robot.Direction.ToString().First()}{(robot.IsLost ? " LOST" : string.Empty)}";
        }
    }
}
