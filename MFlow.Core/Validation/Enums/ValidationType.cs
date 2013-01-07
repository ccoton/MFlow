﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFlow.Core.Validation.Enums
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
        IsShorterThan = 23
    }
}