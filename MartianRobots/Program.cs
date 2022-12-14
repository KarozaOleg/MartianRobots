using MartianRobots.Interface;
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
        private static Dictionary<int, List<IRobotCommand>> RobotsCommands { get; }
        private static Map Map { get; }

        static Program()
        {
            try
            {
                Logger = LogManager.GetCurrentClassLogger();

                var inputDataRepository = new InputDataStringsRepository();
                var inputDataService = new InputDataFromStringsService(inputDataRepository.GetInputDataStrings());
                var inputData = inputDataService.GetInputData();

                InputDataValidateService.Validate(inputData);

                Robots = inputData.Robots;
                RobotsCommands = inputData.RobotsCommands.ToDictionary(c => c.Id, c => c.Commands);

                Map = new Map(inputData.MapWidth, inputData.MapHeight);
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
                    if (RobotsCommands.ContainsKey(robot.Id) == false)
                        continue;
                    foreach (var robotCommand in RobotsCommands[robot.Id])
                    {
                        if (robot.IsLost)
                            break;
                        robotCommand.Execute(robot, Map);
                    }
                }
                foreach (var robot in Robots)
                    Console.WriteLine(OutputService.GetRobotStatus(robot));
            }
            catch(Exception ex)
            {
                Logger?.Error(ex, "main problem");
            }
        }
    }
}
