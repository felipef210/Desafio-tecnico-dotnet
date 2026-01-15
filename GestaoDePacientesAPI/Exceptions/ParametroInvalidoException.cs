namespace GestaoDePacientesAPI.Exceptions;

[Serializable]
public class ParametroInvalidoException : Exception
{
    public ParametroInvalidoException(string message) : base(message) { }
}
