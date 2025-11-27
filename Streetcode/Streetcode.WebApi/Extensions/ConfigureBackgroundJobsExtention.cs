using Hangfire;
using Streetcode.BLL.Services.BlobStorageService;
using Streetcode.WebApi.Utils;

namespace Streetcode.WebApi.Extensions;

public static class ConfigureBackgroundJobsExtention
{
    public static IApplicationBuilder ConfigureBackgroundJobs(this IApplicationBuilder app)
    {
        BackgroundJob.Schedule<WebParsingUtils>(
            wp => wp.ParseZipFileFromWebAsync(),
            TimeSpan.FromMinutes(1));

        RecurringJob.AddOrUpdate<WebParsingUtils>(
            "parse-zip-file-monthly",
            wp => wp.ParseZipFileFromWebAsync(),
            Cron.Monthly);

        RecurringJob.AddOrUpdate<BlobService>(
            "clean-blob-storage-monthly",
            b => b.CleanBlobStorage(),
            Cron.Monthly);

        return app;
    }
}