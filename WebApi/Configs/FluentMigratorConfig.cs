using FluentMigrator.Runner;

namespace WebApi.Configs
{
    public static class FluentMigratorRunner
    {
        public static void RunMigrations(this WebApplication app)
        {
            var conection = app.Configuration["ConnectionStrings:SqlServer"];
            var serviceProvider = new ServiceCollection()
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb.AddSqlServer2016()
                    .WithGlobalConnectionString(conection)
                    .ScanIn(typeof(Migrations.Migration001).Assembly).For.Migrations())
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                .BuildServiceProvider(false);

            using (var scope = serviceProvider.CreateScope())
            {
                var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();

                if (runner.HasMigrationsToApplyUp())
                {
                    runner.MigrateUp();
                }
            }
        }
    }
}
