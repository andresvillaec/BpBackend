namespace Account.Domain.Exceptions;

public class MovementNotFoundException : Exception
{
    public MovementNotFoundException(int id)
        : base($"El movimiento con id {id} no existe")
    {
    }
}
