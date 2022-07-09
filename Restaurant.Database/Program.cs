IConfigurationRoot configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

string connectionStrings = configuration.GetSection("ConnectionStrings").GetValue<string>("Default");

UpgradeEngine upgrader = DeployChanges.To
                .SqlDatabase(connectionStrings)
                .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                .WithTransaction()
                .JournalToSqlTable("dbo", "SchemaVersions")
                .LogToConsole()
                .Build();

Console.WriteLine("Starting upgrader...");

EnsureDatabase.For.SqlDatabase(connectionStrings);

List<SqlScript> scriptsToExecute = upgrader.GetScriptsToExecute();
if (scriptsToExecute.Any())
    Console.WriteLine("Scripts to execute:");
Console.ForegroundColor = ConsoleColor.Yellow;
foreach (SqlScript scriptToExecute in scriptsToExecute)
    Console.WriteLine("- " + scriptToExecute.Name);
Console.ResetColor();

DatabaseUpgradeResult result = upgrader.PerformUpgrade();

if (!result.Successful)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine(result.Error);
    Console.ResetColor();
    return;
}

Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine(value: "Success!");
Console.ResetColor();
