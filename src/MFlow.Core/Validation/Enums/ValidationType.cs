﻿namespace MFlow.Core.Validation.Enums
{
    public enum ValidationType
    {
        Unknown = 999,
        NotEmpty = 0,
        Equal = 1,
        NotEqual = 2,
        LessThan = 3,
        GreaterThan = 4,
        LessThanOrEqualTo = 5,
        GreaterThanOrEqualTo = 6,
        RegEx = 7,
        IsEmail = 8,
        Contains = 9,
        Before = 10,
        After = 11, 
        On = 12,
        IsRequired = 13,
        IsLength = 14,
        IsCreditCard = 15,
        IsPostCode = 16,
        IsZipCode = 17,
        IsThisYear = 18,
        IsThisMonth = 19,
        IsThisWeek = 20,
        IsToday = 21,
        IsLongerThan = 22,
        IsShorterThan = 23,
        IsNumeric = 24,
        IsAlpha = 25,
        IsDate = 26,
        Any = 27,
        None = 28,
        IsNotNull = 29,
        IsPassword = 30,
        IsUsername = 31,
        IsUrl = 32,
        IsBetween = 33,
        All = 34,
        IsSame = 35
    }
}
