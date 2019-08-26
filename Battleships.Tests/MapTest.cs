using Battleships.Enums;
using Battleships.Factories;
using Battleships.Models;
using Moq;
using Xunit;

namespace Battleships.Tests
{
    public class MapTest
    {
        [Fact]
        public void Load_ShouldRequestRandomShips()
        {
            // Arange
            var mapConfiguration = new MapConfiguration
            {
                BattleshipsCount = 1,
                DestroyersCount = 2,
                Height = 10,
                Width = 10
            };

            var battleShipFactoryMock = new Mock<IBattleShipFactory>();
            battleShipFactoryMock
                .Setup(battleShipFactory => battleShipFactory.GetRandomShip(mapConfiguration.Width, mapConfiguration.Height))
                .Returns(new Ship(new Location { X = 7, Y = 2 }, Axis.Vertical, ShipType.BattleShip));

            var destroyerShipFactoryMock = new Mock<IDestroyerShipFactory>();
            destroyerShipFactoryMock
                .SetupSequence(destroyerShipFactory => destroyerShipFactory.GetRandomShip(mapConfiguration.Width, mapConfiguration.Height))
                .Returns(new Ship(new Location { X = 1, Y = 2 }, Axis.Horizontal, ShipType.Destroyer))
                .Returns(new Ship(new Location { X = 1, Y = 3 }, Axis.Horizontal, ShipType.Destroyer));

            var map = new Map(battleShipFactoryMock.Object, destroyerShipFactoryMock.Object, mapConfiguration);

            // Act
            map.Load();

            //Assert
            battleShipFactoryMock.Verify(
                battleShipFactory => battleShipFactory.GetRandomShip(mapConfiguration.Width, mapConfiguration.Height), 
                Times.Exactly(1));

            destroyerShipFactoryMock.Verify(
                destroyerShipFactory => destroyerShipFactory.GetRandomShip(mapConfiguration.Width, mapConfiguration.Height),
                Times.Exactly(2));
        }
    }
}
