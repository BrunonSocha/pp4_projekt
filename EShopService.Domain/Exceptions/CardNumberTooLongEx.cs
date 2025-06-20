namespace EShopService.Domain.Exceptions;

public class CardNumberTooLongEx : Exception
{
    public CardNumberTooLongEx()
    {
    }

    public CardNumberTooLongEx(string message) : base("The card number is too long - 19 digit limit exceeded.")
    {
    }

    public CardNumberTooLongEx(string message, Exception inner) : base(message, inner)
    {
    }
}