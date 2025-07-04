namespace WorkerService.Hosteds
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private Timer _timer;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation(" ----> Start do processo. ----> ");

            _timer = new Timer(ExecuteProcess, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));

            // Garantir que a task continue até o cancelamento
            return Task.CompletedTask;
        }

        private void ExecuteProcess(object state)
        {
            _logger.LogInformation(" ----> Execute do processo. ----> ");
            _logger.LogInformation($"{DateTime.Now}");
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation(" <---- Stop do processo. <---- ");
            _logger.LogInformation($"{DateTime.Now}");

            _timer?.Change(Timeout.Infinite, 0);
            _timer?.Dispose();

            return base.StopAsync(cancellationToken);
        }
    }
}
