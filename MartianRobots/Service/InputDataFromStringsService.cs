using MartianRobots.Interface;
using MartianRobots.Model;
using System;

namespace MartianRobots.Service
{
    internal class InputDataFromStringsService : IInputDataService
    {
        private string[] InputDataStrings { get; }
        private InputData InputData { get; }

        public InputDataFromStringsService(string[] inputDataStrings)
        {
            InputDataStrings = inputDataStrings ?? throw new ArgumentNullException(nameof(inputDataStrings));
        }

        public InputData GetInputData()
        {
            if(InputData == null)
            {
                throw new NotImplementedException();
            }
            return InputData;
        }
    }
}
