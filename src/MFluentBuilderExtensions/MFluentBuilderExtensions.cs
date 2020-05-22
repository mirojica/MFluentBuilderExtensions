using System;
using System.Collections.Generic;
using MFluentBuilderExtensions.Domain;

namespace MFluentBuilderExtensions
{
    public static class MFluentBuilderExtensions
    {
        public static T With<T>(this T obj, string name, object value) where T : class =>
            Members<T>.Of(obj).TrySetValueForMemberWith(name, value).Object;

        public static T With<T>(this T obj, Action<T> action)
        {
            action(obj);
            return obj;
        }

        public static T With<T>(this T obj, object value) =>
            Members<T>.Of(obj).TrySetValueForMemberWithSameTypeAs(value).Object;

        public static T Generate<T>(this T obj) where T : class =>
            Members<T>.Of(obj).GenerateRandomValueForUnassignedMembers().Object;

        public static List<T> And<T>(this T obj, T additionalObj) =>
            new List<T> { obj, additionalObj };

        public static List<T> And<T>(this List<T> list, T additionalObj)
        {
            list.Add(additionalObj);
            return list;
        }
    }
}
