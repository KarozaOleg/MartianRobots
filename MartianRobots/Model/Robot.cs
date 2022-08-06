namespace MartianRobots.Model
{
    public class Robot
    {
        public int Id { get; }
        public Direction Direction { get; private set; }
        public Coordinates Coordinates { get; private set; }
        public bool IsLost { get; private set; }

        public Robot(int id, Direction direction, Coordinates coordinates)
        {
            Id = id;
            Direction = direction;
            Coordinates = coordinates;
        }

        public void SetDirection(Direction direction)
        {
            Direction = direction;
        }

        public void SetCoordinates(Coordinates coordinates)
        {
            Coordinates = coordinates;
        }

        public void SetIsLostToTrue()
        {
            IsLost = true;
        }

        public override string ToString()
        {
            return $"{Coordinates}{(IsLost ? " LOST" : string.Empty)}";
        }
    }
}
