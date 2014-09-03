using System;
using System.Configuration;
using System.Linq;
using NUnit.Framework;
using InjectionMap.Configuration.Test.Data;

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
        public void LoadConfiguration()
        {
            using (var mapper = new InjectionMapper())
            {
                mapper.Initialize();
            }

            using (var resolver = new InjectionResolver())
            {
                var keyOne = resolver.Resolve<IKeyOne>();
                Assert.IsNotNull(keyOne);

                var keyTwo = resolver.Resolve<IKeyTwo>();
                Assert.IsNotNull(keyTwo);

                var typeOne = resolver.Resolve<ObjectTypeOne>();
                Assert.IsNotNull(typeOne);
            }
        }

        [Test]
        public void LoadConfigurationWithoutMapInitializer()
        {
            using (var mapper = new InjectionMapper())
            {
                mapper.Initialize();
            }

            using (var resolver = new InjectionResolver())
            {
                var keyThree = resolver.Resolve<IKeyThree>();
                Assert.IsNull(keyThree);
            }
        }
    }
}
