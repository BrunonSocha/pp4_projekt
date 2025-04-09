using Xunit;
using EShop.Application;
namespace EShop.Application.Tests;

public class CardCheckerTests
{
    [Theory]
    [InlineData("3497 7965 8312 797", true)]
    [InlineData("4024-0071-6540-1778", true)]
    [InlineData("5530016454538418", true)]
    public void ValidateCard_Validates_Correct(string cardNumber, bool expected)
    {
        // No arrangement needed as CardChecker is a static class

        // Act
        bool result = CardChecker.ValidateCard(cardNumber);

        // Assert
        Assert.Equal(expected, result);

    }

    [Theory]
    [InlineData("3497.7965.8312.797", false)]
    [InlineData("5555555555555555", false)]
    [InlineData("lmao", false)]
    [InlineData("1111", false)]
    public void ValidateCard_Validates_Incorrect(string cardNumber, bool expected)
    {
        // No arrangement needed as CardChecker is a static class

        // Act
        bool result = CardChecker.ValidateCard(cardNumber);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("American Express", "3497 7965 8312 797")]
    [InlineData("American Express", "345-470-784-783-010")]
    [InlineData("American Express", "378523393817437")]
    [InlineData("Visa", "4024-0071-6540-1778")]
    [InlineData("Visa", "4532 2080 2150 4434")]
    [InlineData("Visa", "4532289052809181")]
    [InlineData("MasterCard", "5530016454538418")]
    [InlineData("MasterCard", "5551561443896215")]
    [InlineData("MasterCard", "5131208517986691")]
    public void GetCardType_Gets_CorrectType(string cardType, string cardNumber)
    {
        // No arrangement needed as CardChecker is a static class

        // Act
        string result = CardChecker.GetCardType(cardNumber);

        // Assert
        Assert.Equal(cardType, result);
    }

    [Theory]
    [InlineData("Not a correct card.", "lmao")]
    [InlineData("Not a correct card.", "1111")]
    [InlineData("Not a correct card.", "6221 2345 6789 0123​​")]
    [InlineData("Unknown card type.", "1111111111111117")]
    [InlineData("Unknown card type.", "1111-1111-1111-1117")]
    public void GetCardType_Checks_IncorrectNumbers(string cardType, string cardNumber)
    {
        // No arrangement needed as CardChecker is a static class

        // Act
        string result = CardChecker.GetCardType(cardNumber);

        // Assert
        Assert.Equal(cardType, result);
    }

}