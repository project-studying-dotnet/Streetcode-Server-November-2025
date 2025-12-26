using Microsoft.Extensions.Options;
using Quartz;

namespace Streetcode.Auth.Infrastructure.Jobs.DeleteRevokedRefreshTokenJob.Configurations;

public class DeleteRevokedRefreshTokensJobConfiguration : IConfigureOptions<QuartzOptions>
{
    public void Configure(QuartzOptions options)
    {
        var jobKey = JobKey.Create(nameof(DeleteRevokedRefreshTokensJob));
        options
            .AddJob<DeleteRevokedRefreshTokensJob>(jobBuilder => jobBuilder.WithIdentity(jobKey))
            .AddTrigger(trigger =>
                trigger
                    .ForJob(jobKey)
                    .WithSimpleSchedule(schedule =>
                        schedule
                            .WithInterval(TimeSpan.FromDays(7.00))
                            .RepeatForever())
            );
    }
}