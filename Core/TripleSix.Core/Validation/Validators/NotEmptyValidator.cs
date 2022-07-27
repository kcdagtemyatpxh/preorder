﻿using System.Collections;
using FluentValidation;
using FluentValidation.Validators;
using TripleSix.Core.Helpers;

namespace TripleSix.Core.Validation.Validators
{
    public class NotEmptyValidator<T, TProperty> : PropertyValidator<T, TProperty>
    {
        public override string Name => "NotEmptyValidator";

        public override bool IsValid(ValidationContext<T> context, TProperty value)
        {
            switch (value)
            {
                case null:
                case string str when str.IsNullOrWhiteSpace():
                case ICollection { Count: 0 }:
                case Array { Length: 0 }:
                case IEnumerable e when !e.GetEnumerator().MoveNext():
                    return false;
            }

            if (Equals(value, default(TProperty)))
                return false;

            return true;
        }

        protected override string GetDefaultMessageTemplate(string errorCode)
            => "'{PropertyName}' không được bỏ trống";
    }
}
