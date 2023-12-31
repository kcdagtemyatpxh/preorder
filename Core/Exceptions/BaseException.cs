﻿using Microsoft.AspNetCore.Http;
using TripleSix.Core.Helpers;
using TripleSix.Core.WebApi;

namespace TripleSix.Core.Exceptions
{
    /// <summary>
    /// Lỗi cơ bản.
    /// </summary>
    public abstract class BaseException : Exception
    {
        /// <summary>
        /// Khởi tạo <see cref="BaseException"/>.
        /// </summary>
        /// <param name="message">Mô tả lỗi.</param>
        public BaseException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Mã lỗi.
        /// </summary>
        public virtual string Code
        {
            get
            {
                if (GetType() == typeof(BaseException))
                    return "exception";

                var code = GetType().Name;
                if (code.EndsWith("Exception"))
                    code = code[..^9];
                code = code.ToSnakeCase();
                return code;
            }
        }

        /// <summary>
        /// Mã số trạng thái HTTP.
        /// </summary>
        public virtual int HttpCodeStatus => 500;

        /// <summary>
        /// Chuyển đổi thành <see cref=">ErrorResult"/>.
        /// </summary>x`
        /// <param name="httpContext"><see cref=">HttpContext"/>.</param>
        /// <returns><see cref="ErrorResult"/>.</returns>
        public virtual ErrorResult ToErrorResult(HttpContext? httpContext = default)
        {
            return new ErrorResult(HttpCodeStatus, Code, Message);
        }
    }
}
