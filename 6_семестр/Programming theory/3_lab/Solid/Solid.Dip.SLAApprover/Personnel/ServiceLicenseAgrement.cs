namespace Solid.Dip.Personnel;

public sealed class ServiceLicenseAgrement
{
    public int MinUptimePercent { get; }
    public int ProblemResolutionTimeDays { get; }

    public ServiceLicenseAgrement(int minUpdatePercent, int problemResolutionTimeDays)
    {
        this.MinUptimePercent = minUpdatePercent;
        this.ProblemResolutionTimeDays = problemResolutionTimeDays;
    }
}
