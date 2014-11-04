using System;
using NUnit.Framework;

namespace InjectionMap.Configuration.FailTest
{
    [TestFixture]
    public class UnitTest
    {
        [Test]
        [ExpectedException(typeof(ResolverException))]
        public void LoadFailingConfiguration()
        {
            using (var mapper = new MapInitializer())
            {
                mapper.Initialize();
            }
        }
    }
}
