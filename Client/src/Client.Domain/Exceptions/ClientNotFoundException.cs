namespace Client.Domain.Exceptions;

public class ClientNotFoundException : Exception
{
    public ClientNotFoundException(int id) 
        : base($"El cliente con id {id} no existe")
    { 
    }
}
