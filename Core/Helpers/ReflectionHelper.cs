﻿using System.ComponentModel;
using System.Reflection;

namespace TripleSix.Core.Helpers
{
    /// <summary>
    /// Helper xử lý reflection.
    /// </summary>
    public static class ReflectionHelper
    {
        /// <summary>
        /// Lấy danh sách type chỉ định và các type mà nó kế thừa.
        /// </summary>
        /// <param name="type">Type chỉ định.</param>
        /// <returns>Danh sách type.</returns>
        public static IEnumerable<Type> BaseTypesAndSelf(this Type? type)
        {
            while (type != null)
            {
                yield return type;

                if (type.BaseType == null) break;
                type = type.BaseType;
            }
        }

        /// <summary>
        /// Kiểm tra một type có thể null hay không.
        /// </summary>
        /// <param name="type">Type cần kiểm tra.</param>
        /// <returns><c>True</c> nếu type có thể null, ngược lại thì <c>False</c>.</returns>
        public static bool IsNullableType(this Type type)
        {
            return Nullable.GetUnderlyingType(type) != null || type.IsClass;
        }

        /// <summary>
        /// Kiểm tra type có phải là con của 1 raw generic type hay không.
        /// </summary>
        /// <param name="type">Type cần kiểm tra.</param>
        /// <param name="genericType">Raw generic type làm đối chiếu.</param>
        /// <returns><c>True</c> nếu type là con của raw generic type chỉ định, ngược lại là <c>False</c>.</returns>
        public static bool IsSubclassOfRawGeneric(this Type type, Type genericType)
        {
            if (!genericType.IsGenericType)
                return false;

            while (type != null && type != typeof(object))
            {
                var cur = type.IsGenericType ? type.GetGenericTypeDefinition() : type;
                if (genericType == cur)
                    return true;

                if (type.BaseType == null) break;
                type = type.BaseType;
            }

            return false;
        }

        /// <summary>
        /// Lấy raw generic type.
        /// </summary>
        /// <param name="type">Type cần kiểm tra.</param>
        /// <param name="genericType">Raw generic type làm đối chiếu.</param>
        /// <returns>Raw generic type.</returns>
        public static Type? GetRawGeneric(this Type type, Type genericType)
        {
            if (!genericType.IsGenericType)
                throw new ArgumentException("must be generic type", nameof(genericType));

            while (type != null && type != typeof(object))
            {
                var cur = type.IsGenericType ? type.GetGenericTypeDefinition() : type;
                if (genericType == cur)
                    return type;

                if (type.BaseType == null) break;
                type = type.BaseType;
            }

            return null;
        }

        /// <summary>
        /// Lấy chính xác type, nếu type có thể null thì trả về type gốc.
        /// </summary>
        /// <param name="type">Type cần xử lý.</param>
        /// <returns>Type dữ liệu không null.</returns>
        public static Type GetUnderlyingType(this Type type)
        {
            var result = Nullable.GetUnderlyingType(type);
            if (result == null) return type;
            return result;
        }

        /// <summary>
        /// Lấy các property public của type.
        /// </summary>
        /// <param name="type">Type cần xử lý.</param>
        /// <returns>Danh sách property.</returns>
        public static PropertyInfo[] GetPublicProperties(this Type type)
        {
            if (type.IsInterface)
            {
                var propertyInfos = new List<PropertyInfo>();

                var considered = new List<Type>();
                var queue = new Queue<Type>();
                considered.Add(type);
                queue.Enqueue(type);
                while (queue.Count > 0)
                {
                    var subType = queue.Dequeue();
                    foreach (var subInterface in subType.GetInterfaces())
                    {
                        if (considered.Contains(subInterface)) continue;

                        considered.Add(subInterface);
                        queue.Enqueue(subInterface);
                    }

                    var typeProperties = subType.GetProperties(
                        BindingFlags.FlattenHierarchy
                        | BindingFlags.Public
                        | BindingFlags.Instance);

                    var newPropertyInfos = typeProperties
                        .Where(x => !propertyInfos.Contains(x));

                    propertyInfos.InsertRange(0, newPropertyInfos);
                }

                return propertyInfos.ToArray();
            }

            return type.GetProperties(BindingFlags.FlattenHierarchy
                | BindingFlags.Public | BindingFlags.Instance);
        }

        /// <summary>
        /// Lấy tên hiển thị của property được khai báo bằng <see cref="DisplayNameAttribute"/>.
        /// </summary>
        /// <param name="propertyInfo">Property cần xử lý.</param>
        /// <returns>Tên hiển thị của property.</returns>
        public static string GetDisplayName(this MemberInfo propertyInfo)
        {
            var displayName = propertyInfo.GetCustomAttribute<DisplayNameAttribute>();
            if (displayName == null || displayName.DisplayName.IsNullOrWhiteSpace())
                return propertyInfo.Name;

            return displayName.DisplayName.ToTitleCase();
        }
    }
}