using MartianRobots.Interface;
using System;
using System.Collections.Generic;

namespace MartianRobots.Commands
{
    /// <summary>
    /// Represent command for robot, has id of robot and array of commands
    /// </summary>
    public class RobotCommands
    {
        public int Id { get; }
        public List<IRobotCommand> Commands { get; }

        public RobotCommands(int id, List<IRobotCommand> commands)
        {
            Id = id;
            Commands = commands ?? throw new ArgumentNullException(nameof(commands));
        }
    }
}
