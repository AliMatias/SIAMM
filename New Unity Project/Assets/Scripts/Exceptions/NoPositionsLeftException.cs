using System;

//se lanza cuando no se encuentran más posiciones para agregar átomos
public class NoPositionsLeftException : Exception
{
    public NoPositionsLeftException(string message) : base(message) { }
}
