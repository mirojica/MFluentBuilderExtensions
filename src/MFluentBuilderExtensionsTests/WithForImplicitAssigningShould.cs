using NUnit.Framework;
using MFluentBuilderExtensions;
using FluentAssertions;
using System.Collections.Generic;
using System;
using MFluentBuilderExtensionsTests.TestData;

namespace MFluentBuilderExtensionsTests
{
    public class WithForImplicitAssigningShould
    {
        [Test]
        public void Implicitly_assign_value_to_single_public_custom_type()
        {
            var sample = new Sample();
            var publicImpliciteSample = new PublicImpliciteSample(777);

            sample.With(publicImpliciteSample);

            sample.InstanceOfPublicImpliciteSample.Should().BeEquivalentTo(publicImpliciteSample);
        }

        [Test]
        public void Implicitly_assign_value_to_single_private_custom_type()
        {
            var sample = new Sample();
            var privateImpliciteSample = new PrivateImpliciteSample(98);

            sample.With(privateImpliciteSample);

            sample.ValueOfInstanceOfPrivateImpliciteSample.Should().BeEquivalentTo(privateImpliciteSample);
        }

        [Test]
        public void Implicitly_assign_value_to_single_public_built_in_type()
        {
            var sample = new Sample();

            sample.With(5.4M);

            sample.DecimalProperty.Should().Be(5.4M);
        }

        [Test]
        public void Implicitly_assign_value_to_single_private_built_in_type()
        {
            var sample = new Sample();

            sample.With('N');

            sample.ValueOfPrivateCharField.Should().BeEquivalentTo('N');
        }

        [Test]
        public void Implicitly_assign_value_to_single_public_list_of_built_in_type()
        {
            var sample = new Sample();

            sample.With(new List<decimal> { 6.7M, 3.4M });

            sample.PublicListOfDecimal.Should().BeEquivalentTo(new List<decimal> { 6.7M, 3.4M });
        }

        [Test]
        public void Implicitly_assign_value_to_single_private_list_of_built_in_type()
        {
            var sample = new Sample();

            sample.With(new List<char> { 's', 'k' });

            sample.ValueOfPrivateListOfChar.Should().BeEquivalentTo(new List<char> { 's', 'k' });
        }

        [Test]
        public void Implicitly_assign_value_to_single_public_list_of_custom_type()
        {
            var sample = new Sample();
            var listOfPublicImpliciteSample = new PublicImpliciteSample(1).And(new PublicImpliciteSample(2));

            sample.With(listOfPublicImpliciteSample);

            sample.ListOfInstanceOfPublicImpliciteSample.Should().BeEquivalentTo(listOfPublicImpliciteSample);
        }

        [Test]
        public void Implicitly_assign_value_to_single_private_list_of_custom_type()
        {
            var sample = new Sample();
            var listOfPrivateImpliciteSample = new PrivateImpliciteSample(1).And(new PrivateImpliciteSample(2));

            sample.With(listOfPrivateImpliciteSample);

            sample.ValueOfListOfInstanceOfPrivateImpliciteSample.Should().BeEquivalentTo(listOfPrivateImpliciteSample);
        }

        [Test]
        public void Fail_to_implicitly_assign_value_if_there_is_more_then_one_member_of_same_type_as_provided_value()
        {
            var sample = new Sample();

            Action action = () => sample.With("some value");

            action.Should()
                .ThrowExactly<InvalidOperationException>()
                .WithMessage($"There are more then one member of type {typeof(string)}");
        }

        [Test]
        public void Fail_to_implicitly_assign_value_if_there_is_no_member_of_same_type_as_provided_value()
        {
            var sample = new Sample();

            Action action = () => sample.With(new NonExistingMember());

            action.Should()
                .ThrowExactly<MissingMemberException>()
                .WithMessage($"Memebr of type {typeof(NonExistingMember)} does not exist.");
        }
    }
}
