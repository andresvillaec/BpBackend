namespace Account.Domain.Exceptions;

public class NoFundsAvailable : Exception
{
    public NoFundsAvailable()
        : base($"Saldo no disponible")
    {
    }
}
