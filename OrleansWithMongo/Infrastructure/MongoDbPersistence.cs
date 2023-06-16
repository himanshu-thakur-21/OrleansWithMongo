using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Serializers;
using Newtonsoft.Json;
using Orleans.Configuration;
using Orleans.Providers.MongoDB.Configuration;
using Orleans.Providers.MongoDB.StorageProviders.Serializers;
using Orleans.Runtime;
using System.Net;

namespace OrleansWithMongo.Infrastructure
{
    public static class MongoDbPersistence
    {
        #region CONSTANTS

        private const string DB_NAME = "OrleansDemo";
        private const string DB_GRAIN_STORAGE = "MongoDBStore";
        private const string CLUSTER = "HelloWorldCluster";
        private const int SILO_PORT = 11111;
        private const int GATEWAY_PORT = 30000;
        private const bool CREATE_SHARD_KEY = false;

        #endregion

        #region PUBLIC METHODS

        /// <summary>
        /// Add mongo persistence with silo builder
        /// </summary>
        /// <param name="siloBuilder"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static ISiloBuilder ConfigureMongoPersistence(this ISiloBuilder siloBuilder, string connectionString)
        {
            ApplyBsonConfiguration();

            siloBuilder
                .UseMongoDBClient(connectionString)
                .Configure<JsonGrainStateSerializerOptions>(options => options.ConfigureJsonSerializerSettings = settings =>
                {
                    settings.NullValueHandling = NullValueHandling.Include;
                    settings.ObjectCreationHandling = ObjectCreationHandling.Replace;
                    settings.DefaultValueHandling = DefaultValueHandling.Populate;
                })
                .AddMongoDBGrainStorage(DB_GRAIN_STORAGE, options =>
                {
                    options.DatabaseName = DB_NAME;
                    options.CreateShardKeyForCosmos = CREATE_SHARD_KEY;
                })
                .ConfigureServices(services => services
                    .AddSingletonNamedService<IGrainStateSerializer, BsonGrainStateSerializer>(DB_GRAIN_STORAGE));

            return siloBuilder;
        }

        /// <summary>
        /// Use mongo db clustering with mongo persistence
        /// </summary>
        /// <param name="siloBuilder"></param>
        /// <returns></returns>
        public static ISiloBuilder UseMongoClustering(this ISiloBuilder siloBuilder) =>
            siloBuilder           
            .UseMongoDBClustering(options =>
            {
                options.DatabaseName = DB_NAME;
                options.CreateShardKeyForCosmos = CREATE_SHARD_KEY;
            })
            .Configure<ClusterOptions>(options =>
            {
                options.ClusterId = CLUSTER;
                options.ServiceId = CLUSTER;                
            }).ConfigureEndpoints(IPAddress.Loopback, SILO_PORT, GATEWAY_PORT);

        #endregion

        #region PRIVATE METHODS

        /// <summary>
        /// Add convetion packs and mongo type serializers
        /// </summary>
        private static void ApplyBsonConfiguration()
        {
            ConventionRegistry.Register(
                "Default",
                new ConventionPack()
                {
                    new CamelCaseElementNameConvention(),
                    new IgnoreExtraElementsConvention(true)
                },
                t => true);

            BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
        }

        #endregion
    }
}
