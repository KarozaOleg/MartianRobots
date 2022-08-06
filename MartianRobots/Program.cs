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
                var inputDataRepository = new InputDataFromAppMemoryRepository();
                var inputDataService = new InputDataFromStringsService(inputDataRepository.GetInputData());
                var inputData = inputDataService.GetInputData();
                if (inputData == null)
                    throw new ArgumentNullException(nameof(inputData));

                var robotCommandService = new RobotCommandService(inputData.Robots, inputData.RobotsCommands);
                robotCommandService.LaunchAllRobots();
            }
            catch(Exception ex)
            {
                Logger?.Error(ex, "main problem");
            }
        }
    }
}
