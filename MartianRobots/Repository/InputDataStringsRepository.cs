namespace MartianRobots.Repository
{
    /// <summary>
    /// Represent a storage of strings which may be used as input data for application
    /// </summary>
    internal class InputDataStringsRepository
    {
        /// <summary>
        /// Returns array of strings which represents input data for application
        /// </summary>
        /// <returns></returns>
        internal string[] GetInputDataStrings()
        {
            return new string[]
            {
                "5 3",
                "1 1 E",
                "RFRFRFRF",
                "3 2 N",
                "FRRFLLFFRRFLL",
                "0 3 W",
                "LLFFFLFLFL"
            };
        }
    }
}
