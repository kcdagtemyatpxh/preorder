﻿namespace TripleSix.Core.Validation
{
    [AttributeUsage(AttributeTargets.Property)]
    public class NotNullAttribute : Attribute
    {
        /// <summary>
        /// Kiểm tra giá trị của property không được phép null.
        /// </summary>
        public NotNullAttribute()
        {
        }
    }
}
