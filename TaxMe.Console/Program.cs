using TaxMe.Library;

int income = 30000;
int age = 45;
bool isResident = true;
bool TBRLevy = true;
bool LILevy = true;
GeneralTaxationRule generalTaxationRule = new(income, age, isResident, TBRLevy, LILevy);

Console.WriteLine($"Income: {income}");
Console.WriteLine($"Age: {age}");
Console.WriteLine($"Is Resident: {isResident}");
Console.WriteLine($"TBR Levy Applies: {TBRLevy}");
Console.WriteLine($"LI Levy Applies: {LILevy}");
Console.WriteLine();

Console.WriteLine($"Tax           = {generalTaxationRule.Tax}");
Console.WriteLine($"Actual Income = {generalTaxationRule.ActualIncome}");
