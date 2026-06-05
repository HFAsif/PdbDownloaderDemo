namespace PdbDownloader.Sample;

using HelperClass.All;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;


[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
class PdbDownloaderDemoAttribute : Attribute
{
    public PdbDownloaderDemoAttribute() 
    {
        if (typeof(object).GetType().Assembly.GetCustomAttribute(typeof(AssemblyFileVersionAttribute)) is AssemblyFileVersionAttribute assemblyVersionAttribute and not null)
        {
            InternalLogger.MyLogs(assemblyVersionAttribute.Version);
        }

        var runtimeFramework = new Io.DirectoryInfo(RuntimeEnvironment.GetRuntimeDirectory());
        InternalLogger.MyLogs($"Runtime .Net DirName: {runtimeFramework.Name}");

        var msCorLibVersionInfo = FileVersionInfo.GetVersionInfo(typeof(object).Assembly.Location);

        var GelAllProperties = msCorLibVersionInfo.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

        foreach (var property in GelAllProperties)
        {
            try
            {
                var value = property.GetValue(msCorLibVersionInfo, null);
                if (value is not null and not int and not bool and not "")
                {
                    InternalLogger.MyLogs($"{property.Name}: {value}");
                }
               
            }
            catch (Exception ex)
            {
                InternalLogger.MyLogs($"Error retrieving {property.Name}: {ex.Message}");
            }
        }

        //InternalLogger.MyLogs($"Runtime .Net DirName: {msCorLibVersionInfo.ProductVersion}");

        var GelAllPropertiesRuntimeInformation = typeof(RuntimeInformation).GetProperties(BindingFlags.Public | BindingFlags.Static);
        foreach (var property in GelAllPropertiesRuntimeInformation)
        {
            try
            {
                var value = property.GetValue(null, null);
                InternalLogger.MyLogs($"{property.Name}: {value}");
            }
            catch (Exception ex)
            {
                InternalLogger.MyLogs($"Error retrieving {property.Name}: {ex.Message}");
            }
        }

        var version = Environment.Version;
        InternalLogger.MyLogs($"Framework Version: {version}");

        // Returns logical cores/threads available to the application
        int logicalCores = Environment.ProcessorCount;
        InternalLogger.MyLogs($"Logical Cores: {logicalCores}");

        var cpuName = Registry.GetValue(keyName: @"HKEY_LOCAL_MACHINE\HARDWARE\DESCRIPTION\System\CentralProcessor\0", valueName: "ProcessorNameString", defaultValue: null)?.ToString();
        InternalLogger.MyLogs(cpuName);

        
    }

}
