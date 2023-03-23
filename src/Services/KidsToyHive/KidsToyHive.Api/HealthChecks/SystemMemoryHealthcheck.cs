using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Api.HealthChecks;

public class MemoryMetrics
{
    public double Total;
    public double Used;
    public double Free;
}
public class MemoryMetricsClient
{
    public MemoryMetrics GetMetrics()
    {
        MemoryMetrics metrics;
        if (IsUnix())
        {
            metrics = GetUnixMetrics();
        }
        else
        {
            metrics = GetWindowsMetrics();
        }
        return metrics;
    }
    private bool IsUnix()
    {
        var isUnix = RuntimeInformation.IsOSPlatform(OSPlatform.OSX) ||
                     RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
        return isUnix;
    }
    private MemoryMetrics GetWindowsMetrics()
    {
        var output = "";
        var info = new ProcessStartInfo();
        info.FileName = "wmic";
        info.Arguments = "OS get FreePhysicalMemory,TotalVisibleMemorySize /Value";
        info.RedirectStandardOutput = true;
        using (var process = Process.Start(info))
        {
            output = process.StandardOutput.ReadToEnd();
        }
        var lines = output.Trim().Split("\n");
        var freeMemoryParts = lines[0].Split("=", StringSplitOptions.RemoveEmptyEntries);
        var totalMemoryParts = lines[1].Split("=", StringSplitOptions.RemoveEmptyEntries);
        var metrics = new MemoryMetrics();
        metrics.Total = Math.Round(double.Parse(totalMemoryParts[1]) / 1024, 0);
        metrics.Free = Math.Round(double.Parse(freeMemoryParts[1]) / 1024, 0);
        metrics.Used = metrics.Total - metrics.Free;
        return metrics;
    }
    private MemoryMetrics GetUnixMetrics()
    {
        var output = "";
        var info = new ProcessStartInfo("free -m");
        info.FileName = "/bin/bash";
        info.Arguments = "-c \"free -m\"";
        info.RedirectStandardOutput = true;
        using (var process = Process.Start(info))
        {
            output = process.StandardOutput.ReadToEnd();
            Console.WriteLine(output);
        }
        var lines = output.Split("\n");
        var memory = lines[1].Split(" ", StringSplitOptions.RemoveEmptyEntries);
        var metrics = new MemoryMetrics();
        metrics.Total = double.Parse(memory[1]);
        metrics.Used = double.Parse(memory[2]);
        metrics.Free = double.Parse(memory[3]);
        return metrics;
    }
}
public class SystemMemoryHealthcheck : IHealthCheck
{
    public HealthStatus GetHealthStatus(double percentUsed)
    {
        if (percentUsed == 80) return HealthStatus.Degraded;
        if (percentUsed > 80)
            return HealthStatus.Degraded;

        if (percentUsed > 90)
            return HealthStatus.Unhealthy;

        return HealthStatus.Healthy;
    }
    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        var client = new MemoryMetricsClient();
        var metrics = client.GetMetrics();
        var percentUsed = 100 * metrics.Used / metrics.Total;
        var status = GetHealthStatus(percentUsed);
        var data = new Dictionary<string, object>
         {
             { "Total", metrics.Total },
             { "Used", metrics.Used },
             { "Free", metrics.Free }
         };
        var result = new HealthCheckResult(status, null, null, data);
        return await Task.FromResult(result);
    }
}
