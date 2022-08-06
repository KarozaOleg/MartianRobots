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

        static Program()
        {
            try
            {
                Logger = LogManager.GetCurrentClassLogger();                
            }
            catch(Exception ex)
            {
                Logger?.Error(ex, ".ctor problem");
                throw;
            }
        }

        static void Main(string[] args)
        {
            try
            {
                //var inputDataRepository = new InputDataStringsRepository();
                //var inputDataService = new InputDataFromStringsService(inputDataRepository.GetInputData());
                var inputDataService = new InputDataFromAppMemoryService();
                var inputData = inputDataService.GetInputData();
                if (inputData == null)
                    throw new ArgumentNullException(nameof(inputData));

                var map = new Map(inputData.MapWidth, inputData.MapHeight);
                var commandExecutionService = new CommandExecutionService(map);

                var robots = inputData.Robots;
                var robotCommands = inputData.RobotsCommands.ToDictionary(c => c.Id, c => c.Commands);

                foreach (var robot in robots)
                {
                    if (robotCommands.ContainsKey(robot.Id) == false)
                        continue;

                    foreach (var command in robotCommands[robot.Id])
                        commandExecutionService.ExecuteCommand(robot, command.Type, command.Value);
                }

                foreach (var robot in robots)                
                    Console.WriteLine(robot.ToString());
            }
            catch(Exception ex)
            {
                Logger?.Error(ex, "main problem");
            }
        }
    }
}
