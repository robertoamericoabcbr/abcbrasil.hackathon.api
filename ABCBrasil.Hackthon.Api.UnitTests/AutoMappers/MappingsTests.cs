using ABCBrasil.Hackthon.Api.Domain.Mappings;
using AutoMapper;
using System.Reflection;

namespace ABCBrasil.Hackthon.Api.UnitTests.AutoMappers
{
    public class MappingsTests
    {
        public MapperConfiguration Mapper { get; set; }

        public MappingsTests()
        {
            Mapper = new MapperConfiguration(m =>
            {
                var profiles = from g in Assembly.Load(typeof(MappingProfile).Assembly.GetName()).GetTypes()
                               where g.Name.EndsWith("Profile")
                               select g;

                foreach (var profile in profiles)
                {
                    m.AddProfile(profile);
                }
            });
        }

        [Fact]
        public void MapMustHaveValidConfiguration()
        {
            Mapper.AssertConfigurationIsValid();
        }
    }
}