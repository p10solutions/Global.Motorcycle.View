using AutoFixture;
using Global.Delivery.View.Application.Features.Deliverys.Commands.SaveDelivery;

namespace Global.Motorcycle.View.UnitTest.Application.Features.Deliveries.Commands.SaveDelivery
{
    public class SaveDeliveryCommandUnitTest
    {
        readonly Fixture _fixture;

        public SaveDeliveryCommandUnitTest()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void Command_Should_Be_Valid()
        {
            _fixture.Customize<SaveDeliveryCommand>(c => c
                .With(p => p.Document, _fixture.Create<string>().Substring(0, 20))
                .With(p => p.LicenseNumber, _fixture.Create<string>().Substring(0, 20)));

            var command = _fixture.Create<SaveDeliveryCommand>();

            var result = command.Validate();

            Assert.True(result);
        }

        [Fact]
        public void Command_Should_Be_Invalid()
        {
            var command = new SaveDeliveryCommand();

            var result = command.Validate();

            Assert.False(result);
        }
    }
}
