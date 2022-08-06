﻿using System;
using System.Collections.Generic;

namespace MartianRobots.Model
{
    /// <summary>
    /// Represent command for robot, has id of robot and array of commands
    /// </summary>
    public class RobotCommand
    {
        public int Id { get; }
        public List<Command> Commands { get; }

        public RobotCommand(int id, List<Command> commands)
        {
            Id = id;
            Commands = commands ?? throw new ArgumentNullException(nameof(commands));
        }
    }
}
