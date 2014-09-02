using System;
using System.Configuration;
using System.Linq;
using NUnit.Framework;

namespace InjectionMap.Configuration.Test
{
    [TestFixture]
    public class UnitTest
    {
        [Test]
        public void LoadConfigurationWithConfigurationManager()
        {
            var section = ConfigurationManager.GetSection("injectionMap") as InjectionMapSection;
            Assert.IsTrue(section.Mappings.Any());
        }

        [Test]
        public void LoadConfigurationUsingInjectionMapExtension()
        {
            using (var mapper = new InjectionMapper())
            {
                mapper.Initialize();
            }
        }
    }
}
