namespace Account.Domain.Exceptions;

public class AccountNotFoundException : Exception
{
    public AccountNotFoundException(int id)
        : base($"La cuenta con id {id} no existe")
    {
    }

    public AccountNotFoundException(string accountNumber)
        : base($"La número de cuenta {accountNumber} no existe")
    {
    }
}
