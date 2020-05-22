using NUnit.Framework;
using MFluentBuilderExtensions;
using FluentAssertions;
using MFluentBuilderExtensionsTests.TestData;

namespace MFluentBuilderExtensionsTests
{
    public class WithForTypedMembersShould
    {
        [Test]
        public void Assign_value_to_built_in_type_public_property()
        {
            var sample = new Sample();

            sample.With(obj => obj.StringProperty = "some value");

            sample.StringProperty.Should().Be("some value");
        }

        [Test]
        public void Assign_value_to_custom_type_public_property()
        {
            var sample = new Sample();

            sample.With(obj => obj.InstanceOfSubSample = new SubSample(12));

            sample.InstanceOfSubSample.Should().BeEquivalentTo(new SubSample(12));
        }
    }
}
