using InjectionMap.Configuration.Test.Data;
using NUnit.Framework;

namespace InjectionMap.Configuration.MapInitializer.Test
{
    [TestFixture]
    public class UnitTest
    {
        [SetUp]
        public void Setup()
        {
            using (var mapper = new InjectionMapper())
            {
                mapper.Clean<IContractThree>();
            }
        }

        [Test]
        public void LoadConfigurationWithMapinitializer()
        {
            using (var mapper = new InjectionMap.MapInitializer())
            {
                mapper.Initialize();
            }

            using (var resolver = new InjectionResolver())
            {
                var keyThree = resolver.Resolve<IContractThree>();
                Assert.IsNotNull(keyThree);
            }
        }

        [Test]
        public void LoadConfigurationWithMapinitializerToCustomContext()
        {
            var context = new MappingContext();
            using (var mapper = new InjectionMap.MapInitializer(context))
            {
                mapper.Initialize();
            }

            using (var resolver = new InjectionResolver())
            {
                var keyThree = resolver.Resolve<IContractThree>();
                Assert.IsNull(keyThree);
            }

            using (var resolver = new InjectionResolver(context))
            {
                var keyThree = resolver.Resolve<IContractThree>();
                Assert.IsNotNull(keyThree);
            }
        }
    }
}
