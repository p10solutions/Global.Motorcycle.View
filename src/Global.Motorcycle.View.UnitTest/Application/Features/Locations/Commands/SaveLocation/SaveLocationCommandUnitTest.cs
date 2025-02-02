using AutoFixture;
using Global.Motorcycle.View.Application.Features.Rentals.Commands.SaveRental;

namespace Global.Motorcycle.View.UnitTest.Application.Features.Locations.Commands.SaveLocation
{
    public class SaveLocationCommandUnitTest
    {
        readonly Fixture _fixture;

        public SaveLocationCommandUnitTest()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void Command_Should_Be_Valid()
        {
            var command = _fixture.Create<SaveRentalCommand>();

            var result = command.Validate();

            Assert.True(result);
        }

        [Fact]
        public void Command_Should_Be_Invalid()
        {
            var command = new SaveRentalCommand();

            var result = command.Validate();

            Assert.False(result);
        }
    }
}
