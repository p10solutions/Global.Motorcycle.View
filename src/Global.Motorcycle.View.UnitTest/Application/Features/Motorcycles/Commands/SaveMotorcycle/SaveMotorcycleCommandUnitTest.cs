using AutoFixture;
using Global.Motorcycle.View.Application.Features.Motorcycles.Commands.SaveMotorcycle;
using Global.Motorcycle.View.Domain.Entities;

namespace Global.Motorcycle.UnitTest.Application.Features.Motorcycles.Commands.SaveMotorcycle
{
    public class SaveMotorcycleCommandUnitTest
    {
        readonly Fixture _fixture;

        public SaveMotorcycleCommandUnitTest()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void Command_Should_Be_Valid()
        {
            _fixture.Customize<SaveMotorcycleCommand>(c => c
                .With(p => p.Plate, _fixture.Create<string>().Substring(0, 10)));

            var command = _fixture.Create<SaveMotorcycleCommand>();

            var result = command.Validate();

            Assert.True(result);
        }

        [Fact]
        public void Command_Should_Be_Invalid()
        {
            EMotorcycleStatus status = EMotorcycleStatus.Active;

            var command = new SaveMotorcycleCommand(
                Guid.Empty,
                "",
                "572358",
                2023,
                status
            );

            var result = command.Validate();

            Assert.False(result);
        }
    }
}
