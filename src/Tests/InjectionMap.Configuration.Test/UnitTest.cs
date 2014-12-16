using System;
using System.Configuration;
using System.Linq;
using NUnit.Framework;
using InjectionMap.Configuration.Test.Data;
using InjectionMap.Tracing;
using InjectionMap.Configuration.Test.Data.Tracing;

namespace InjectionMap.Configuration.Test
{
    [TestFixture]
    public class UnitTest
    {
        static UnitTest()
        {
            LoggerFactory.LoggerCallback = () => new TraceLogger();
        }

        [SetUp]
        public void Setup()
        {
            using (var mapper = new InjectionMapper())
            {
                mapper.Clean<IContractOne>();
                mapper.Clean<IContractTwo>();
                mapper.Clean<ObjectTypeOne>();
            }
        }

        [Test]
        public void LoadConfigurationWithConfigurationManager()
        {
            var section = ConfigurationManager.GetSection("injectionMap") as InjectionMapSection;
            Assert.IsTrue(section.Mappings.Any());
        }

        [Test]
        public void LoadConfiguration()
        {
            using (var mapper = new MapInitializer())
            {
                mapper.Initialize();
            }

            using (var resolver = new InjectionResolver())
            {
                var keyOne = resolver.Resolve<IContractOne>();
                Assert.IsNotNull(keyOne);

                var keyTwo = resolver.Resolve<IContractTwo>();
                Assert.IsNotNull(keyTwo);

                var typeOne = resolver.Resolve<ObjectTypeOne>();
                Assert.IsNotNull(typeOne);
            }
        }

        [Test]
        public void LoadConfigurationWithoutMapInitializer()
        {
            using (var mapper = new MapInitializer())
            {
                mapper.Initialize();
            }

            using (var resolver = new InjectionResolver())
            {
                var keyThree = resolver.Resolve<IContractThree>();
                Assert.IsNull(keyThree);
            }
        }

        [Test]
        public void LoadConfigurationToCustomContext()
        {
            var context = new MappingContext();
            using (var mapper = new MapInitializer(context))
            {
                mapper.Initialize();
            }

            using (var resolver = new InjectionResolver())
            {
                // default context has to deliver null
                var keyOne = resolver.Resolve<IContractOne>();
                Assert.IsNull(keyOne);

                var keyTwo = resolver.Resolve<IContractTwo>();
                Assert.IsNull(keyTwo);

                //var typeOne = resolver.Resolve<ObjectTypeOne>();
                //Assert.IsNull(typeOne);
            }

            using (var resolver = new InjectionResolver(context))
            {
                // resolve from custom context
                var keyOne = resolver.Resolve<IContractOne>();
                Assert.IsNotNull(keyOne);

                var keyTwo = resolver.Resolve<IContractTwo>();
                Assert.IsNotNull(keyTwo);

                //var typeOne = resolver.Resolve<ObjectTypeOne>();
                //Assert.IsNotNull(typeOne);
            }
        }

        [Test]
        public void PropertyInjectionTest()
        {
            using (var mapper = new MapInitializer())
            {
                mapper.Initialize();
            }

            using (var resolver = new InjectionResolver())
            {
                var obj = resolver.Resolve<IObjectWithProperty>();

                Assert.IsNotNull(obj.Contract);
                Assert.IsTrue(obj.Contract is ObjectTypeOne);

                Assert.IsNotNull(obj.ContractOne);
                Assert.IsTrue(obj.ContractOne is ObjectTypeOne);
            }
        }
    }
}
