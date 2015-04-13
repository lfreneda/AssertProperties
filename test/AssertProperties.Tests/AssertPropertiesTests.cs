using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace AssertProperties.Tests
{
    [TestFixture]
    public class AssertPropertiesTests
    {
        public class Foo
        {
            public string Name { get; set; }
        }

        [Test]
        [ExpectedException(typeof(AssertExpcetion), ExpectedMessage = "\r\nName expected to be NULL but was Luiz\r\n")]
        public void ShouldBeNull_WhenPropertyIsNotNull_ShouldThrowException()
        {
            var foo = new Foo { Name = "Luiz" };

            foo.AssertProperties()
                    .EnsureThat(f => f.Name).ShouldBeNull()
                .Assert();
        }
    }
}
