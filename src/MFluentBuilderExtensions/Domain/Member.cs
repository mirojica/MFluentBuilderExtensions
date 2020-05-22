using System;
using System.Linq;

namespace MFluentBuilderExtensions.Domain
{
    internal abstract class Member
    {
        public abstract string Name { get; }
        public abstract Type Type { get; }

        internal abstract bool IsNotAssigned(object obj);

        internal abstract void SetRandomValue(object obj);

        internal abstract void SetValue(object obj, object value);

        protected static object ValueFor(Type type, string name)
        {
            object value;
            if (type.Equals(typeof(string)))
                value = name;
            else if (type.Equals(typeof(int)))
                value = name.Count();
            else if (type.IsValueType)
                value =  Activator.CreateInstance(type);
            else
            {
                var constructorParameters = type.GetConstructors().First().GetParameters()
                    .Select(parameter => ValueFor(parameter.ParameterType, parameter.Name))
                    .ToArray();
                value = Activator.CreateInstance(type, constructorParameters);
            }

            return Convert.ChangeType(value, type);
        }

        protected void EnsureValueAndMemberAreTheSameType(Type memberType, object value)
        {
            if (!memberType.Equals(value.GetType()))
                throw new FormatException($"Cannot assign value of type {value.GetType()} to {memberType}");
        }

        protected static bool IsNotAssigned(Type type, object value) => type.IsPrimitive ?
                value == null || value.Equals(Activator.CreateInstance(type)) : value == null;
    }
}
