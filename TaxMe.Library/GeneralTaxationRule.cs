namespace TaxMe.Library;

public class GeneralTaxationRule
{
    private readonly int _lowThreshold = 18200;
    private readonly int _firstTier = 45000;
    private readonly int _firstTierBase = 5092;
    private readonly int _secondTier = 120000;
    private readonly int _secondTierBase = 29467;
    private readonly int _thirdTier = 180000;
    private readonly int _thirdTierBase = 51667;
    public double ActualIncome { get; }
    public double Tax { get; private set; }

    public GeneralTaxationRule(int income, int age, bool isResident, bool TBRLevy, bool LILevy)
    {
        ActualIncome = income - ReturnTax(income, age, isResident, TBRLevy, LILevy);
    }

    private double ReturnTax(int income, int age, bool isResident, bool TBRLevy, bool LILevy)
    {
        var stage1 = 0.19;
        var stage2 = 0.325;
        var stage3 = 0.37;
        var stage4 = 0.45;
        var medicareLevyCharge = 0.02;
        var TBRLevyCharge = 0.02;
        if (income <= _lowThreshold)
        {
            return Tax = 0.0;
        }
        else if (income <= _firstTier)
        {
            if (LILevy)
            {
                if (income > 20543)
                {
                    return Tax = MedicareTax(isResident, income, 20543, stage1, medicareLevyCharge);
                }
                else
                {
                    return Tax = 0;
                }
            }
            else
            {
                return Tax = MedicareTax(isResident, income, _lowThreshold, stage1, medicareLevyCharge);
            }
        }
        else if (income <= _secondTier)
        {
            return Tax = _firstTierBase + MedicareTax(isResident, income, _firstTier, stage2, medicareLevyCharge);
        }
        else if (income <= _thirdTier)
        {
            return Tax = _secondTierBase + MedicareTax(isResident, income, _secondTier, stage3, medicareLevyCharge);
        }
        else
        {
            if (TBRLevy)
            {
                return Tax = _thirdTierBase + MedicareTax(isResident, income, _thirdTier, stage4, medicareLevyCharge) +
                             (income - _thirdTierBase) * TBRLevyCharge;
            }
            return Tax = _thirdTierBase + MedicareTax(isResident, income, _thirdTier, stage4, medicareLevyCharge);
        }
    }

    public static double MedicareTax(bool isResident, int income, int threshold, double taxRate, double additionalTax)
    {
        return isResident ? (income - threshold) * taxRate + income * additionalTax : (income - threshold) * taxRate;
    }
}
