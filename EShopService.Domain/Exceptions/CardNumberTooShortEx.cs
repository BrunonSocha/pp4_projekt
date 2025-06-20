namespace EShopService.Domain.Exceptions;

public class CardNumberTooShortEx : Exception
{
    public CardNumberTooShortEx()
    {
    }

    public CardNumberTooShortEx(string message) : base("The card number is too short. It should contain more than 13 digits.")
    {
    }

    public CardNumberTooShortEx(string message, Exception inner) : base(message, inner)
    {
    }
}