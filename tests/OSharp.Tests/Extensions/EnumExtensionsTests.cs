using System.ComponentModel;
using Xunit;

namespace OSharp.Extensions.Tests
{
    public class EnumExtensionsTests
    {
        [Fact]
        public void ToDescriptionTest()
        {
            TestEnum value = TestEnum.EnumItemA;
            Assert.Equal("枚举项A", value.ToDescription());

            value = TestEnum.EnumItemB;
            Assert.Equal("EnumItemB", value.ToDescription());
        }

        private enum TestEnum
        {
            [Description("枚举项A")]
            EnumItemA,
            EnumItemB,
        }
    }
}