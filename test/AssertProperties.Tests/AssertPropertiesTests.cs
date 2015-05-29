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
        [ExpectedException(typeof(AssertException), ExpectedMessage = "\r\nName expected to be NULL but was Luiz\r\n")]
        public void ShouldBeNull_WhenPropertyIsNotNull_ShouldThrowException()
        {
            var foo = new Foo { Name = "Luiz" };

            foo.AssertProperties()
                    .EnsureThat(f => f.Name).ShouldBeNull()
                .Assert();
        }

        [Test]
        [ExpectedException(typeof(AssertException), ExpectedMessage = "\r\nName expected NOT to be NULL\r\n")]
        public void ShouldNotBeNull_WhenPropertyIsNull_ShouldThrowException()
        {
            var foo = new Foo { Name = null };

            foo.AssertProperties()
                    .EnsureThat(f => f.Name).ShouldNotBeNull()
                .Assert();
        }

        [Test]
        [ExpectedException(typeof(AssertException), ExpectedMessage = "\r\nName expected to be Freneda but was Luiz\r\n")]
        public void ShouldBe_GivenNameLuizWhenShouldBeFreneda_ShouldThrowException()
        {
            var foo = new Foo { Name = "Luiz" };

            foo.AssertProperties()
                    .EnsureThat(f => f.Name).ShouldBe("Freneda")
                .Assert();
        }
    }
}
