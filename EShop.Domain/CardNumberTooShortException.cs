public class CardNumberTooShortException : Exception
{
    public CardNumberTooShortException()
    {
    }

    public CardNumberTooShortException(string message) : base("The card number is too short. It should contain more than 13 digits.")
    {
    }

    public CardNumberTooShortException(string message, Exception inner) : base(message, inner)
    {
    }
}