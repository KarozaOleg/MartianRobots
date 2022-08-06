using System;
using System.Collections.Generic;

namespace MartianRobots.Model
{
    public class RobotCommand
    {
        public int Id { get; }
        public List<(CommandType Type, Command Value)> Commands { get; }

        public RobotCommand(int id, List<(CommandType type, Command value)> commands)
        {
            Id = id;
            Commands = commands ?? throw new ArgumentNullException(nameof(commands));
        }
    }
}
