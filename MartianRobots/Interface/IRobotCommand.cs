using MartianRobots.Model;

namespace MartianRobots.Interface
{
    public interface IRobotCommand
    {
        public void Execute(Robot robot, Map map);
    }
}
