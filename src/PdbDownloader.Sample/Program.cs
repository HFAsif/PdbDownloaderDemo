
using HelperClass;
using HelperClass.All;
using PdbDownloader.Sample;
using System;
using System.Threading.Tasks;
using System.Reflection;

internal class Program : ProgramBase
{
    private static async Task Main(string[] args)
    {
        var _ = Enm.PdbSignetureLoadType.VS;
        var __ = Enm.DownloadType.Bazzed;

        var currentDir = Io.Path.GetDirectoryName(typeof(Program).Assembly.Location);
        PdbDownloadHelper.GetPdbInfos(currentDir, out var SymbolDir, out var downloadItems, _, [_clrSystemLocation, _clrjitSystemLocation]);
        using (DownloaderExDemoTest downloaderExDemoTest = new DownloaderExDemoTest() { downloadType = __, downloadItems = downloadItems })
        {
#if NET35
            TaskEx
#else
        Task
#endif
                .Run(async () => await downloaderExDemoTest.Run()).GetAwaiter().GetResult();

        }

        InternalLogger.MyLogs("All downloads completed. Press any key to exit...");
        Console.ReadLine();

        }

    }

