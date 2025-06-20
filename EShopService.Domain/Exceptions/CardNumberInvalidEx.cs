namespace EShopService.Domain.Exceptions;

public class CardNumberInvalidEx : Exception
{
    public CardNumberInvalidEx()
    {
    }

    public CardNumberInvalidEx(string message) : base(message)
    {
    }

    public CardNumberInvalidEx(string message, Exception inner) : base(message, inner)
    {
    }
}
