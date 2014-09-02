using System;
using NUnit.Framework;

namespace InjectionMap.Configuration.FailTest
{
    [TestFixture]
    public class UnitTest
    {
        [Test]
        [ExpectedException(typeof(InjectionMap.Exceptions.MappingMismatchException))]
        public void LoadFailingConfiguration()
        {
            using (var mapper = new InjectionMapper())
            {
                mapper.Initialize();
            }
        }
    }
}
