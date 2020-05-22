using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace MFluentBuilderExtensions.Domain
{
    public class Members<T>
    {
        private List<Member> _members;
        public T _obj;

        public T Object => _obj;

        private Members(T obj, List<Member> members)
        {
            _members = members.ToList();
            _obj = obj;
        }

        internal static Members<T> Of(T obj)
        {
            var members = new List<Member>();

            members.AddRange(GetWritablePropertiesFrom(obj));
            members.AddRange(GetAllFields(obj));

            return new Members<T>(obj, members);
        }

        internal Members<T> GenerateRandomValueForUnassignedMembers()
        {
            _members.Where(member => member.IsNotAssigned(_obj)).ToList()
                .ForEach(member => member.SetRandomValue(_obj));

            return this;
        }

        internal Members<T> TrySetValueForMemberWithSameTypeAs(object value)
        {
            var members = _members.Where(member => member.Type.Equals(value.GetType()));
            EnsureThereIsExactlyOneMemberOfTypeAs(value, members);

            members.First().SetValue(_obj, value);

            return this;
        }

        internal Members<T> TrySetValueForMemberWith(string name, object value)
        {
            var member = _members.Where(member => member.Name.Equals(name)).FirstOrDefault();
            if (member == null)
                throw new MissingMemberException($"Member with name {name} does not exist.");

            member.SetValue(_obj, value);

            return this;
        }

        private static void EnsureThereIsExactlyOneMemberOfTypeAs(object value, IEnumerable<Member> members)
        {
            if (members.Count() > 1)
                throw new InvalidOperationException($"There are more then one member of type {value.GetType()}");
            else if (!members.Any())
                throw new MissingMemberException($"Memebr of type {value.GetType()} does not exist.");
        }

        private static IEnumerable<Member> GetAllFields(T obj) =>
            obj.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(field => !field.CustomAttributes.Any(atribute =>
                    atribute.AttributeType.Equals(typeof(CompilerGeneratedAttribute))))
                            .Select(fieldInfo => Field.For(fieldInfo));

        private static IEnumerable<Member> GetWritablePropertiesFrom(T obj) =>
            obj.GetType().GetProperties()
                .Where(property => property.CanWrite)
                .Select(propertyInfo => Property.For(propertyInfo));
    }
}