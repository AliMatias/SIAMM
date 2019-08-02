using System;

//se lanza cuando no se encuentran más posiciones para agregar átomos
public class SpawnException : Exception
{
    public SpawnException(string message) : base(message) { }
}
