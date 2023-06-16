namespace OrleansWithMongo.Infrastructure
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder RegisterPipelines(this IApplicationBuilder app)
        {
            return app
                    .UseSwagger()
                    .UseSwaggerUI()
                    .UseHttpsRedirection()
                    .UseAuthorization();
        }
    }
}
