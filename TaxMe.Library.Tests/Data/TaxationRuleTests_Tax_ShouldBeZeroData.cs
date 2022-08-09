using System.Collections;

namespace TaxMe.Library.Tests.Data;

/// <summary>
/// a class for generating arguments for the TaxationRuleTests tests for the Tax_ShouldBeZero method
/// </summary>
internal sealed class TaxationRuleTests_Tax_ShouldBeZeroData : IEnumerable<object[]>
{
    /// <summary>
    /// used to enumerate between the sets of arguements
    /// </summary>
    /// <returns>a set of arguments</returns>
    public IEnumerator<object[]> GetEnumerator()
    {
        // the incomes to test
        int[] incomes = { -1 };
        // the ages to test
        int[] ages = { 17, 18 };
        bool[] decisions = { true, false };
        // foreach income, age, & levy, combination return a set of arguments
        foreach (var income in incomes)
            foreach (var age in ages)
                foreach (var isResident in decisions)
                    foreach (var tbrLevy in decisions)
                        foreach (var liLevy in decisions)
                            yield return new object[] { income, age, isResident, tbrLevy, liLevy };
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
