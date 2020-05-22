using FluentAssertions;
using NUnit.Framework;
using MFluentBuilderExtensions;
using System.Collections.Generic;
using System;
using MFluentBuilderExtensionsTests.TestData;

namespace MFluentBuilderExtensionsTests
{
    public class WithShould
    {
        [Test]
        public void Assign_value_to_built_in_type_public_property()
        {
            var sample = new Sample();

            sample
                .With("StringProperty", "some value")
                .With("IntProperty", 5)
                .With("BoolProperty", true);

            sample.StringProperty.Should().Be("some value");
            sample.IntProperty.Should().Be(5);
            sample.BoolProperty.Should().BeTrue();
        }

        [Test]
        public void Assign_value_to_built_in_type_private_field()
        {
            var sample = new Sample();

            sample
                .With("privateIntField", 5)
                .With("privateStringField", "some string");

            sample.ValueOfPrivateIntField.Should().Be(5);
            sample.ValueOfPrivateStringField.Should().Be("some string");
        }

        [Test]
        public void Assign_value_to_public_list_of_built_in_types()
        {
            var sample = new Sample();

            sample.With("PublicListOfStrings", "first".And("second").And("third"));

            sample.PublicListOfStrings.Should().BeEquivalentTo(new List<string> { "first", "second", "third" });
        }

        [Test]
        public void Assign_value_to_private_list_of_built_in_types()
        {
            var sample = new Sample();

            sample.With("privateListOfStrings", "first".And("second").And("third"));

            sample.ValueOfPrivateListOfStrings.Should().BeEquivalentTo(new List<string> { "first", "second", "third" });
        }

        [Test]
        public void Assign_value_to_non_built_in_type_public_property()
        {
            var sample = new Sample();
            var instanceOfSubSample = new SubSample(40000);

            sample.With("InstanceOfSubSample", instanceOfSubSample);

            sample.InstanceOfSubSample.Should().BeEquivalentTo(instanceOfSubSample);
        }

        [Test]
        public void Assign_value_to_non_built_in_type_private_field()
        {
            var sample = new Sample();
            var instanceOfSubSample = new SubSample(30000);

            sample.With("privateInstanceOfSubSample", instanceOfSubSample);

            sample.ValueOfPrivateInstanceOfSubSample.Should().BeEquivalentTo(instanceOfSubSample);
        }

        [Test]
        public void Assign_value_to_public_list_of_custom_types()
        {
            var sample = new Sample();

            sample.With(
                "PublicListOfInstanceOfSubSample",
                new SubSample(777).And(new SubSample(888)).And(new SubSample(999)));

            sample.PublicListOfInstanceOfSubSample.Should().HaveCount(3).And.AllBeOfType<SubSample>();
        }

        [Test]
        public void Assign_value_to_private_list_of_custom_types()
        {
            var sample = new Sample();

            sample.With(
                "privateListOfInstanceOfSubSample",
                new SubSample(111).And(new SubSample(222)).And(new SubSample(333)));

            sample.ValueOfPrivateListOfInstanceOfSubSample.Should().HaveCount(3).And.AllBeOfType<SubSample>();
        }

        [Test]
        public void Fail_when_there_is_no_member_with_provided_name()
        {
            var sample = new Sample();

            Action action = () => sample.With("UnknownMember", "some value");

            action.Should()
                .ThrowExactly<MissingMemberException>()
                .WithMessage("Member with name UnknownMember does not exist.");
        }

        [Test]
        public void Fail_when_value_is_not_of_correct_type()
        {
            var sample = new Sample();

            Action actionOnPublicProperty = () => sample.With("BoolProperty", 33);
            Action actionOnPrivateField = () => sample.With("privateStringField", 33);

            actionOnPublicProperty.Should()
                .ThrowExactly<FormatException>()
                .WithMessage("Cannot assign value of type System.Int32 to System.Boolean");
            actionOnPrivateField.Should()
                .ThrowExactly<FormatException>()
                .WithMessage("Cannot assign value of type System.Int32 to System.String");
        }
    }
}
