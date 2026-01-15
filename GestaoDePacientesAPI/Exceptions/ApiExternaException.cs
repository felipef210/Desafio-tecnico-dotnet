namespace GestaoDePacientesAPI.Exceptions;

[Serializable]
public class ApiExternaException : Exception
{
    public ApiExternaException(string message) : base(message) { }
}
