using InjectionMap.Configuration.Test.Data;
using NUnit.Framework;

namespace InjectionMap.Configuration.MapInitializer.Test
{
    [TestFixture]
    public class UnitTest
    {
        [Test]
        public void LoadConfigurationWithMapinitializer()
        {
            using (var mapper = new InjectionMapper())
            {
                mapper.Initialize();
            }

            using (var resolver = new InjectionResolver())
            {
                var keyThree = resolver.Resolve<IKeyThree>();
                Assert.IsNotNull(keyThree);
            }
        }
    }
}
