namespace TaxMe.Library.Tests.Models;

/// <summary>
/// a class for modelling thresholds
/// </summary>
internal sealed class ThresholdModel
{
    /// <summary>
    /// the lower bound of the threshold
    /// </summary>
    public int LB { get; }
    /// <summary>
    /// the upper bound of the threshold
    /// </summary>
    public int UB { get; }
    /// <summary>
    /// the amount of tax without any levies as percentile to apply within the range.
    /// x : LB <= x <= UB
    /// </summary>
    public double Tax { get; }
    /// <summary>
    /// the amount of tax without levies to apply for all the thresholds below this one
    /// </summary>
    public int Base { get; }
    /// <summary>
    /// whether this range applies for low income
    /// </summary>
    public bool LI { get; }

    public ThresholdModel(int lb, int ub, double tax, int @base, bool lI = false)
    {
        LB = lb;
        UB = ub;
        Tax = tax;
        Base = @base;
        LI = lI;
    }

    public override string ToString()
    {
        return $"{LB} <= {UB} - {Tax} {Base} LI {LI}";
    }
}
