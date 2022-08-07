using MartianRobots.Model;

namespace MartianRobots.Tests
{
    public class MapTests
    {
        [Theory]
        [InlineData(33, 33, Orientation.North)]
        [InlineData(0, 0, Orientation.South)]
        public void SetAddDropOffMove_AddingDropOffMove_MapReturnsScent(int x, int y, Orientation orientation)
        {
            // Arrange
            var map = new Map(33, 33);
            var coordinates = new Coordinates(x, y);

            // Act
            map.SetAddDropOffMove(coordinates, orientation);

            // Assert
            Assert.True(map.GetIsMoveWillDropOff(coordinates, orientation));
        }

        [Theory]
        [InlineData(34, 1)]
        [InlineData(-1, 0)]
        [InlineData(1, 55)]
        [InlineData(1, -5)]
        public void IsCoordinatesOutOfMap_WrongInput_ReturnMarkOutOfMap(int x, int y)
        {
            // Arrange
            var map = new Map(33, 33);
            var coordinates = new Coordinates(x, y);

            // Act
            var result = map.IsCoordinatesOutOfMap(coordinates);

            // Assert
            Assert.True(result);
        }
    }
}
