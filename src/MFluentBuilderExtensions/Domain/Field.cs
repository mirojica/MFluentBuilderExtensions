using System;
using System.Reflection;

namespace MFluentBuilderExtensions.Domain
{
    internal class Field : Member
    {
        private FieldInfo _fieldInfo;
        public override string Name => _fieldInfo.Name;
        public override Type Type => _fieldInfo.FieldType;

        public Field(FieldInfo fieldInfo) => _fieldInfo = fieldInfo;

        internal static Member For(FieldInfo fieldInfo) => new Field(fieldInfo);

        internal override void SetValue(object obj, object value)
        {
            EnsureValueAndMemberAreTheSameType(_fieldInfo.FieldType, value);

            _fieldInfo.SetValue(obj, Convert.ChangeType(value, _fieldInfo.FieldType));
        }

        internal override bool IsNotAssigned(object obj) =>
            IsNotAssigned(_fieldInfo.FieldType, _fieldInfo.GetValue(obj));

        internal override void SetRandomValue(object obj) =>
            SetValue(obj, ValueFor(_fieldInfo.FieldType, Name));
    }
}
