﻿using System.Diagnostics.CodeAnalysis;

namespace TripleSix.Core.Helpers
{
    /// <summary>
    /// Helper xử lý danh sách.
    /// </summary>
    public static class CollectionHelper
    {
        /// <summary>
        /// Kiểm tra danh sách có null hay rỗng không.
        /// </summary>
        /// <typeparam name="T">Loại dữ liệu.</typeparam>
        /// <param name="list">Danh sách cần kiểm tra.</param>
        /// <returns>true nếu danh sách null hoặc rổng, ngược lại là false.</returns>
        public static bool IsNullOrEmpty<T>([NotNullWhen(false)] this IEnumerable<T>? list)
        {
            return list == null || !list.Any();
        }

        /// <summary>
        /// Ghép danh sách thành chuỗi, mỗi phần tử sẽ cách nhau theo ký tự chỉ định.
        /// </summary>
        /// <typeparam name="T">Loại dữ liệu.</typeparam>
        /// <param name="list">Danh sách cần xử lý.</param>
        /// <param name="separator">Chuỗi phân tách.</param>
        /// <returns>Chuỗi sau khi được ghép từ danh sách.</returns>
        public static string ToString<T>(this IEnumerable<T> list, string separator)
        {
            return string.Join(separator, list.Where(x => x != null && !x.ToString().IsNullOrWhiteSpace()));
        }
    }
}