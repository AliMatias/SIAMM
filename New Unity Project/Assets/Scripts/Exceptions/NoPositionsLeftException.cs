using System;

public class NoPositionsLeftException : Exception
{
    public NoPositionsLeftException(string message) : base(message) { }
}
