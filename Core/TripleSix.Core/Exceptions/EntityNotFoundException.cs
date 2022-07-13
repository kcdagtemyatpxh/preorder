﻿using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using System.Web;
using TripleSix.Core.Helpers;
using TripleSix.Core.WebApi;

namespace TripleSix.Core.Exceptions
{
    /// <summary>
    /// Lỗi không tìm thấy entity.
    /// </summary>
    public class EntityNotFoundException : BaseException
    {
        /// <summary>
        /// Khởi tạo <see cref="EntityNotFoundException"/>.
        /// </summary>
        /// <param name="entityType">Loại entity.</param>
        /// <param name="query">Câu query gây lỗi.</param>
        /// <param name="message"><inheritdoc/></param>
        public EntityNotFoundException(
            Type entityType,
            IQueryable? query = null,
            string? message = null)
            : base(message.IsNullOrWhiteSpace() ? $"{entityType.Name} not found" : message)
        {
            EntityType = entityType;
            Query = query;
        }

        /// <summary>
        /// Loại entity.
        /// </summary>
        public virtual Type EntityType { get; }

        /// <summary>
        /// Câu query gây lỗi.
        /// </summary>
        public virtual IQueryable? Query { get; }

        /// <inheritdoc/>
        public override int HttpCodeStatus => 404;

        /// <inheritdoc/>
        public override ErrorResult ToErrorResult(HttpContext? httpContext = null)
        {
            var error = base.ToErrorResult(httpContext);

            if (Query != null)
                error.Detail.Add(Query.ToQueryString());

            return error;
        }
    }
}
