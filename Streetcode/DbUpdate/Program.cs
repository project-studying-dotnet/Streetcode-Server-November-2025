namespace Streetcode.DbUpdate
{
    using DbUp;
    using Microsoft.Extensions.Configuration;

    public static class Program
    {
        static int Main(string[] args)
        {
            string migrationPath = Path.Combine(Directory.GetCurrentDirectory(),
                "Streetcode.DAL", "Persistence", "ScriptsMigration");

            string seedPath = Path.Combine(Directory.GetCurrentDirectory(),
                "Streetcode.DAL", "Persistence", "ScriptsSeed");

            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Local";

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "Streetcode.WebApi"))
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables("STREETCODE_")
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            string pathToScript = "";
            string userInput = "";

            Console.WriteLine("Enter '-m' to MIGRATE or '-s' to SEED db:");
            if (userInput == "-m")
            {
                pathToScript = migrationPath;
                Console.WriteLine("Starting Database Migration...");
            }
            else if (userInput == "-s")
            {
                pathToScript = seedPath;
                Console.WriteLine("Starting Database Seeding...");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Invalid command. Exiting.");
                Console.ResetColor();
                return 1;
            }

            var upgrader =
                DeployChanges.To
                    .SqlDatabase(connectionString)
                    .WithScriptsFromFileSystem(pathToScript)
                    .LogToConsole()
                    .Build();

            var result = upgrader.PerformUpgrade();

            if (!result.Successful)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(result.Error);
                Console.ResetColor();
#if DEBUG
                Console.ReadLine();
#endif
                return -1;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Success!");
            Console.ResetColor();
            return 0;
        }
    }
}