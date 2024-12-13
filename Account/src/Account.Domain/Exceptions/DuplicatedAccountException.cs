namespace Account.Domain.Exceptions;

public class DuplicatedAccountException : Exception
{
    public DuplicatedAccountException(string accountNumber)
        : base($"El número de cuenta {accountNumber} ya se encuentra registrado")
    {
    }
}
