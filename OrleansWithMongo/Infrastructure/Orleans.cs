namespace OrleansWithMongo.Infrastructure
{
    public static class Orleans
    {
        public static IHostBuilder ConfigureOrleans(this IHostBuilder hostBuilder, IConfiguration configuration)
        {
            _ = bool.TryParse(configuration["Silo:UseMongoClustering"], out bool useMongoClustering);
            var connectionString = configuration.GetConnectionString("OrleansDemo") ?? "mongodb://localhost:27017";

            hostBuilder.UseOrleans(silo =>
            {
                if (useMongoClustering)
                    silo
                    .UseMongoClustering()
                    .ConfigureMongoPersistence(connectionString);
                else
                    silo
                    .UseLocalhostClustering()
                    .ConfigureMongoPersistence(connectionString);
            });

            return hostBuilder;
        }
    }
}
