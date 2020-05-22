using System.Collections.Generic;
using FluentAssertions;
using MFluentBuilderExtensions;
using MFluentBuilderExtensionsTests.TestData;
using NUnit.Framework;

namespace MFluentBuilderExtensionsTests
{
    public class AndShould
    {
        [Test]
        public void Add_item_in_list_of_built_in_types()
        {
            "one".And("two").Should().BeEquivalentTo(new List<string> { "one", "two" });
        }

        [Test]
        public void Add_item_in_list_of_custom_types()
        {
            new SubSample(4).And(new SubSample(10))
                .Should()
                .BeEquivalentTo(new List<SubSample>
                {
                    new SubSample(4),
                    new SubSample(10)
                });
        }
    }
}
