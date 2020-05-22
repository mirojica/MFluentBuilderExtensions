using System;
using System.Reflection;

namespace MFluentBuilderExtensions.Domain
{
    internal class Property : Member
    {
        private PropertyInfo _propertyInfo;
        public override string Name => _propertyInfo.Name;
        public override Type Type => _propertyInfo.PropertyType;

        public Property(PropertyInfo propertyInfo) => _propertyInfo = propertyInfo;

        internal static Member For(PropertyInfo propertyInfo) => new Property(propertyInfo);

        internal override void SetValue(object obj, object value)
        {
            EnsureValueAndMemberAreTheSameType(_propertyInfo.PropertyType, value);

            _propertyInfo.SetValue(obj, Convert.ChangeType(value, _propertyInfo.PropertyType));
        }

        internal override bool IsNotAssigned(object obj) =>
            IsNotAssigned(_propertyInfo.PropertyType, _propertyInfo.GetValue(obj));

        internal override void SetRandomValue(object obj) =>
            SetValue(obj, ValueFor(_propertyInfo.PropertyType, Name));
    }
}
