﻿#pragma warning disable SA1201 // Elements should appear in the correct order

using System;
using System.ComponentModel;
using TripleSix.Core.Attributes;

namespace TripleSix.Core.Dto
{
    public class FilterParameterDatetime : IFilterParameter
    {
        [DisplayName("[parameter-display-name]")]
        public DateTime[] Value { get; set; }

        [DisplayName("loại lọc của [parameter-name]")]
        [EnumValidate]
        public FilterParameterDatetimeOperators Operator { get; set; } = FilterParameterDatetimeOperators.Is;
    }

    public enum FilterParameterDatetimeOperators
    {
        [Description("=")]
        Is = 1,

        [Description("bắt đầu lúc")]
        Begin = 2,

        [Description("kết thúc lúc")]
        End = 3,

        [Description("trong khoảng")]
        Between = 4,

        [Description("là NULL")]
        IsNull = 5,

        [Description("khác")]
        NotIs = -1,

        [Description("không bắt đầu lúc")]
        NotBegin = -2,

        [Description("không kết thúc lúc")]
        NotEnd = -3,

        [Description("không trong khoảng")]
        NotBetween = -4,

        [Description("không NULL")]
        NotNull = -5,
    }
}
