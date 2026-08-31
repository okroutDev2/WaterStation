using System.Reflection;
using System.Runtime.InteropServices;

namespace WaterStation.Infrastructure;

/// <summary>
/// Non-secret, environment-level application facts for the settings screen.
/// The version is always read from the executing assembly (never hardcoded).
/// </summary>
public static class ApplicationInfo
{
    public const string AppName = "نظام إدارة وفوترة محطة المياه";

    public static string Version { get; } = ReadAssemblyVersion();

    public static string DotNetRuntime => RuntimeInformation.FrameworkDescription;

    public static string MachineName => Environment.MachineName;

    public static string OperatingSystem => RuntimeInformation.OSDescription;

    public static string UICulture => System.Globalization.CultureInfo.CurrentUICulture.Name;

    public static string BuildFlavor =>
#if DEBUG
        "Debug (نسخة تطوير)";
#else
        "Release (نسخة إنتاج)";
#endif

    private static string ReadAssemblyVersion()
    {
        var assembly = Assembly.GetExecutingAssembly();
        var version = assembly.GetName().Version;
        return version is null ? "غير متوفر" : version.ToString();
    }
}