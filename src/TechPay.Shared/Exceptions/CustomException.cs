﻿using System;

namespace TechPay.Shared.Exceptions
{
    public class CustomException : Exception
    {
        public CustomException(string message) : base(message) { }
    }
}

