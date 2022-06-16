namespace Solid.Dip.Personnel;

public sealed class Subcontractor
{
    private readonly string name;

    public Subcontractor(string name)
    {
        this.name = name;
    }

    public bool ApproveSLA(ServiceLicenseAgrement sla)
    {
        var canApprove = CanApprove(sla);
        Console.WriteLine($"SLA approval for {this.name}: {canApprove}");
        return canApprove;
    }

    private static bool CanApprove(ServiceLicenseAgrement sla)
    {
        return 90 <= sla.MinUptimePercent &&
            sla.ProblemResolutionTimeDays <= 2;
    }
}
