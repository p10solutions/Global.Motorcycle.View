using AutoFixture;
using Global.Motorcycle.View.Application.Features.Motorcycles.Commands.DeleteMotorcycle;

namespace Global.Motorcycle.View.UnitTest.Application.Features.Motorcycles.Commands.DeleteMotorcycle
{
    public class DeleteMotorcycleCommandUnitTest
    {
        readonly Fixture _fixture;

        public DeleteMotorcycleCommandUnitTest()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void Command_Should_Be_Valid()
        {
            var command = _fixture.Create<DeleteMotorcycleCommand>();

            var result = command.Validate();

            Assert.True(result);
        }

        [Fact]
        public void Command_Should_Be_Invalid()
        {
            var command = new DeleteMotorcycleCommand(Guid.Empty);

            var result = command.Validate();

            Assert.False(result);
        }
    }
}
