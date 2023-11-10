﻿using System;

namespace RU.NSU.FIT.VirtualManager.Domain.Exceptions;

public class BaseException : Exception
{
    public BaseException(string message) : base(message)
    {
    }

    public BaseException(string message, Exception? innerException) : base(message, innerException)
    {
    }
}