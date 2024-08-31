using AutoFixture;
using Global.Motorcycle.View.Application.Features.Motorcycles.Queries.GetMotorcycle;

namespace Global.Motorcycle.UnitTest.Application.Features.Motorcycles.Queries.GetById
{
    public class GetMotorcycleByIdQueryUnitTest
    {
        readonly Fixture _fixture;

        public GetMotorcycleByIdQueryUnitTest()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void Command_Should_Be_Valid()
        {
            var command = _fixture.Create<GetMotorcycleQuery>();

            var result = command.Validate();

            Assert.True(result);
        }
    }
}
