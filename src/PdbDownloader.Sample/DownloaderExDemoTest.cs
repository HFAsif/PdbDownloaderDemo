namespace PdbDownloader.Sample;

using HelperClass.All;
using HFDownloaderNetEx.Demo;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;

[PdbDownloaderDemo]
partial class DownloaderExDemoTest 
{
    public required Enm.DownloadType downloadType;
    public required IList<DownloadItem> downloadItems;

    public async Task Run()
    {
        foreach (var downloadItem in downloadItems)
        {
            if (downloadItem.FileName is null)
            {
                Debugger.Break();
                throw new GettingExceptions(nameof(downloadItem), " error ");
            }

            var fileInfo = new Io.FileInfo(downloadItem.FileName);

            if (fileInfo.Exists && !HelperViewsStatic.IsNullOrWhiteSpace(Io.File.ReadAllText(downloadItem.FileName)) && fileInfo.Length > 0 && PdbDownloadHelper.PdbFileSizeIsCorrect(downloadItem.FileName))
            {
                InternalLogger.MyLogs("Loaded: " + Io.Path.GetFileName(fileInfo.FullName));
                continue;
            }
            else
            {
                DownloaderExDemo downloaderExDemo = new DownloaderExDemo(downloadType, downloadItem);
                await downloaderExDemo.StartDownloadAsynce();
                InternalLogger.MyLogs("Loaded: " + Io.Path.GetFileName(fileInfo.FullName));
            }
        }


    }

}

partial class DownloaderExDemoTest : IDisposable
{
    private bool disposedValue1;

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue1)
        {
            if (disposing)
            {
                // TODO: dispose managed state (managed objects)
                GetType().GetCustomAttribute<PdbDownloaderDemoAttribute>();


            }

            // TODO: free unmanaged resources (unmanaged objects) and override finalizer
            // TODO: set large fields to null
            disposedValue1 = true;
        }
    }

    // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
    // ~DownloaderExDemoTest()
    // {
    //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
    //     Dispose(disposing: false);
    // }

    void IDisposable.Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
