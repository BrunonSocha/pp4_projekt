public class CardNumberTooLongException : Exception
{
    public CardNumberTooLongException()
    {
    }

    public CardNumberTooLongException(string message) : base("The card number is too long - 19 digit limit exceeded.")
    {
    }

    public CardNumberTooLongException(string message, Exception inner) : base(message, inner)
    {
    }
}