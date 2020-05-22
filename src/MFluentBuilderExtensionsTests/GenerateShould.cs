using FluentAssertions;
using MFluentBuilderExtensions;
using MFluentBuilderExtensionsTests.TestData;
using NUnit.Framework;

namespace MFluentBuilderExtensionsTests
{
    public class GenerateShould
    {
        [Test]
        public void Assign_random_value_to_members_with_built_in_type()
        {
            var sample = new Sample();

            sample.Generate();

            sample.ValueOfPrivateIntField.Should().BeGreaterThan(0);
            sample.ValueOfPrivateStringField.Should().Be("privateStringField");

            sample.BoolProperty.Should().BeFalse();
            sample.IntProperty.Should().BeGreaterThan(0);
            sample.StringProperty.Should().Be(nameof(sample.StringProperty));
        }

        [Test]
        public void Assign_empty_list_of_built_in_type()
        {
            var sample = new Sample();

            sample.Generate();

            sample.PublicListOfStrings.Should().BeEmpty();
            sample.ValueOfPrivateListOfStrings.Should().BeEmpty();
        }

        [Test]
        public void Skip_assigning_to_already_assigned_member_of_built_in_type()
        {
            var sample = new Sample();
            sample
                .With("IntProperty", 5000)
                .With("privateStringField", "random string");

            sample.Generate();

            sample.IntProperty.Should().Be(5000);
            sample.ValueOfPrivateStringField.Should().Be("random string");
        }

        [Test]
        public void Assign_random_value_to_members_with_non_built_in_type()
        {
            var sample = new Sample();

            sample.Generate();

            sample.InstanceOfSubSample.Should().BeOfType<SubSample>();
            sample.ValueOfPrivateInstanceOfSubSample.Should().BeOfType<SubSample>();
        }

        [Test]
        public void Skip_assigning_to_already_assigned_members_of_non_built_in_type()
        {
            var sample = new Sample();
            var instanceOfSubSample = new SubSample(50000);
            sample
                .With("privateInstanceOfSubSample", instanceOfSubSample)
                .With("InstanceOfSubSample", instanceOfSubSample);

            sample.Generate();

            sample.ValueOfPrivateInstanceOfSubSample.Should().BeEquivalentTo(instanceOfSubSample);
            sample.InstanceOfSubSample.Should().BeEquivalentTo(instanceOfSubSample);
        }
    }
}
