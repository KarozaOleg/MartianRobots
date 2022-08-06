using MartianRobots.Model;
using MartianRobots.Repository;
using MartianRobots.Service;
using NLog;
using System;
using System.Collections.Generic;

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
                var inputDataRepository = new InputDataFromAppMemoryRepository();
                var inputDataService = new InputDataFromStringsService(inputDataRepository.GetInputData());
                var inputData = inputDataService.GetInputData();
                if (inputData == null)
                    throw new ArgumentNullException(nameof(inputData));

                var map = new Map(inputData.MapWidth, inputData.MapHeight);
                var robots = inputData.Robots;
                var robotCommandService = new RobotCommandService(map, robots, inputData.RobotsCommands);
                robotCommandService.LaunchAllRobots();

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
