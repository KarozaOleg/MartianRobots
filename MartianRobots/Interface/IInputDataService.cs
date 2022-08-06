using MartianRobots.Model;

namespace MartianRobots.Interface
{
    public interface IInputDataService
    {
        /// <summary>
        /// Returns InputData instance for application
        /// </summary>
        /// <returns></returns>
        public InputData GetInputData();
    }
}
