using System.Collections;

namespace TaxMe.Library.Tests.Data;

/// <summary>
/// a class for generating arguments for the TaxationRuleTests tests for the Tax_ShouldCalculate method
/// </summary>
internal sealed class TaxationRuleTests_Tax_ShouldCalculateData : IEnumerable<object[]>
{
    /// <summary>
    /// the percentile that the medicare levy applies
    /// </summary>
    private static readonly double _medicare = 0.02D;
    /// <summary>
    /// the percetile that the TBR levy applies
    /// </summary>
    private static readonly double _TBRLevy = 0.02D;
    /// <summary>
    /// the ages to test with
    /// </summary>
    private static readonly int[] _ages = { 17, 18 };

    /// <summary>
    /// an array of all the thresholds
    /// </summary>
    private static readonly ThresholdModel[] _thresholds =
    {
        new(0, 18200, 0, 0), // no tax
        new(0, 20543, 0, 0, lI: true), // no tax for low income
        new(18201, 45000, 0.19D, 0), // tier 1
        new(20544, 45000, 0.19D, 0, lI: true), // tier 1 for low income
        new(45001, 120000, 0.325D, 5092), // tier 2
        new(120001, 180000, 0.37D, 29467), // tier 3
        new(180001, int.MaxValue, 0.45D, 51667) // tier 4
    };


    /// <summary>
    /// calculates the expected tax given at set of inputs
    /// </summary>
    /// <param name="income">the user's income</param>
    /// <param name="age">the user's age</param>
    /// <param name="medicare">whether to apply the medicare levy if applicable</param>
    /// <param name="tbr">whether to apply the TBR levy if applicable</param>
    /// <param name="li">whether to apply the LI levy if applicable</param>
    /// <returns>the expected tax</returns>
    private static double ExpectedTax(int income, int age, bool medicare, bool tbr, bool li)
    {
        // if the user has set low income and meets age requirements, then use low income levy
        li &= age > 17;
        // find the first applicable threshold with the income within its range
        ThresholdModel threshold = _thresholds.First(x => x.LB <= income && income <= x.UB && x.LI == li);
        // add the tbr percentile to the tax rate if it applies
        double tbrOffset = 0;
        if (tbr && income > _thresholds[^1].LB - 1)
            tbrOffset = _TBRLevy;
        // calculate the tax of income in excess of the lower bound
        double tax = (income - threshold.LB + 1) * (threshold.Tax + tbrOffset) + threshold.Base;
        // apply medicare levy if it applies
        if (tax > 0 && medicare)
            tax += income * _medicare;
        return tax;
    }

    /// <summary>
    /// builds a set of arguments for a test case
    /// </summary>
    /// <param name="income">the user's income</param>
    /// <param name="age">the user's age</param>
    /// <param name="medicare">whether to apply the medicare levy if applicable</param>
    /// <param name="tbr">whether to apply the TBR levy if applicable</param>
    /// <param name="li">whether to apply the LI levy if applicable</param>
    /// <returns>the arguments</returns>
    private static object[] Arguments(int income, int age, bool medicare, bool tbr, bool li)
    {
        double tax = ExpectedTax(income, age, medicare, tbr, li);
        return new object[] { income, age, medicare, tbr, li, tax };
    }

    /// <summary>
    /// used to enumerate between the sets of arguements
    /// </summary>
    /// <returns>a set of arguments</returns>
    public IEnumerator<object[]> GetEnumerator()
    {
        // used to toggle the medicare & TBR levies independently
        bool[] decisions = { true, false };

        //foreach threshold add the lower and upper bounds as income arguments for the test
        foreach (var threshold in _thresholds)
            // foreach medicare, tbr, & age, state use all combinations as arguments for the test
            foreach (var medicare in decisions)
                foreach (var tbr in decisions)
                    foreach (var age in _ages)
                    {
                        // construct some the arguments for the test using low income if its in a low income threshold
                        yield return Arguments(threshold.LB, age, medicare, tbr, threshold.LI);
                        yield return Arguments(threshold.UB, age, medicare, tbr, threshold.LI);
                    }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
