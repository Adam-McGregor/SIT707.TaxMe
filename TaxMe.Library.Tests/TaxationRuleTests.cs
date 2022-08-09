namespace TaxMe.Library.Tests;

/// <summary>
/// Tests for the GeneralTaxationRule class
/// </summary>
public static class TaxationRuleTests
{
    /// <summary>
    /// the precision to use in the tests for floating point numbers
    /// </summary>
    private static readonly int _precision = 10;

    /// <summary>
    /// Tests whether the tax & actual income are calcualated correctly
    /// </summary>
    /// <param name="income">the user's income</param>
    /// <param name="age">the user's age</param>
    /// <param name="isResident">whether the user is a resident</param>
    /// <param name="tbrLevy">whether to apply the TBR levy if applicable</param>
    /// <param name="liLevy">whether to apply the low income levy if applicable</param>
    /// <param name="expectedTax">the expected tax value that should be calculated</param>
    [Theory]
    [ClassData(typeof(TaxationRuleTests_Tax_ShouldCalculateData))]
    public static void Tax_ShouldCalculate(int income, int age, bool isResident, bool tbrLevy, bool liLevy, double expectedTax)
    {
        //arange
        double expectedActualIncome = income - expectedTax;

        //act
        GeneralTaxationRule taxationRule = new(income, age, isResident, tbrLevy, liLevy);
        var actualTax = taxationRule.Tax;
        var actualActualIncome = taxationRule.ActualIncome;

        //assert
        Assert.Equal(expectedTax, actualTax, _precision);
        Assert.Equal(expectedActualIncome, actualActualIncome, _precision);
    }

    /// <summary>
    /// Tests that negative inputs return 0
    /// </summary>
    /// <param name="income">the user's income</param>
    /// <param name="age">the user's age</param>
    /// <param name="isResident">whether the user is a resident</param>
    /// <param name="tbrLevy">whether to apply the TBR levy if applicable</param>
    /// <param name="liLevy">whether to apply the low income levy if applicable</param>
    [Theory]
    [ClassData(typeof(TaxationRuleTests_Tax_ShouldBeZeroData))]
    public static void Tax_ShouldBeZero(int income, int age, bool isResident, bool tbrLevy, bool liLevy)
    {
        //arrange
        double expectedTax = 0;
        double expectedActualIncome = 0;

        //act
        GeneralTaxationRule taxationRule = new(income, age, isResident, tbrLevy, liLevy);
        var actualTax = taxationRule.Tax;
        var actualActualIncome = taxationRule.ActualIncome;

        //assert
        Assert.Equal(expectedTax, actualTax, _precision);
        Assert.Equal(expectedActualIncome, actualActualIncome, _precision);
    }
}


