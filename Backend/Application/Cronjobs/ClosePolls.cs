using Application.Commands;
using Cronos;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sgbj.Cron;

namespace Application.Cronjobs
{
    public class ClosePolls : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public ClosePolls(IServiceScopeFactory serviceScopeFactory) : base()
        {
            _serviceScopeFactory = serviceScopeFactory;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var mediatR = scope.ServiceProvider.GetService<IMediator>();
            // Every minute
            using var timer = new CronTimer(CronExpression.Parse("* * * * *"));

            while (await timer.WaitForNextTickAsync(stoppingToken))
            {
                var res = await mediatR.Send(new CloseExpiredPollsCommand());
                Console.WriteLine($"Autoclosed {res} polls");
            }
        }
    }
}
