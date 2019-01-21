using System;

namespace AutomataNETjuegos.Compilador
{
    public interface ITempFileManager : IDisposable
    {
        string Create();
    }
}