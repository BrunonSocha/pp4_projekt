public class CardNumberInvalidException : Exception
{
    public CardNumberInvalidException()
    {
    }

    public CardNumberInvalidException(string message) : base(message)
    {
    }

    public CardNumberInvalidException(string message, Exception inner) : base(message, inner)
    {
    }
}