namespace Solid.Dip.Documents;

public class Payslip : ITxtExportable
{
    public string EmployeeName { get; }
    public int MonthlyIncome { get; }
    public Month Month { get; }

    public Payslip(Personnel.Employee employee, Month month)
    {
        this.EmployeeName = employee.FullName;
        this.MonthlyIncome = employee.MonthlyIncome;
        this.Month = month;
    }

    public string ToTxt()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine($"Month: {this.Month}");
        sb.AppendLine($"Name: {this.EmployeeName}");
        sb.AppendLine($"Income: {this.MonthlyIncome}");
        return sb.ToString();
    }
}
