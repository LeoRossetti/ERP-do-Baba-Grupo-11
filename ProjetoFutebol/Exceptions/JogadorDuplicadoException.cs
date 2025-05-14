namespace Exceptions;
public class JogadorDuplicadoException : Exception
{
    public JogadorDuplicadoException(string msg) : base(msg) { }
}