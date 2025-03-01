using AutoMapper;
using Infrastructure.Mappings;

namespace UnitTests.Infraestructure.Mappings
{
    [TestClass]
    public class MappingProfileTests
    {
        private readonly IMapper _mapper;

        public MappingProfileTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = config.CreateMapper();
        }

        [TestMethod]
        public void MappingProfile_IsValid()
        {
            // Act & Assert
            _mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }
    }
}
