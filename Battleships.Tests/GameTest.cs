using Moq;
using Xunit;

namespace Battleships.Tests
{
    public class GameTest
    {
        [Fact]
        public void Start_ShouldCallMapLoading()
        {
            // Arrange
            var mapMock = new Mock<IMap>();
            var uiMock = new Mock<IUserInterface>();

            var game = new Game(mapMock.Object, uiMock.Object);
            // Act
            game.Start();

            // Assert
            mapMock.Verify(map => map.Load(), Times.AtLeastOnce());
        }
    }
}
