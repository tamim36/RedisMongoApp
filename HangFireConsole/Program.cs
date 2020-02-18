using System;
using System.Collections.Generic;
using System.Threading.Channels;
using Hangfire;
using Hangfire.Logging;
using Hangfire.Logging.LogProviders;
using Hangfire.MemoryStorage;
using Model;
using MongoDB.Bson;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace HangFireConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //Product myProduct = new Product(){PrimaryId = "12345", Name = "Kacchi", Price = 10.3, counter = 3};
            //cache.StringSet("test5", product);
            //cache.SetAdd("test6", JsonConvert.SerializeObject(myProduct));
            //var value = cache.StringGet("test5");
            //Console.WriteLine(value);


            JobStorage storage = new MemoryStorage(new MemoryStorageOptions());
            LogProvider.SetCurrentLogProvider(new ColouredConsoleLogProvider());
            var serverOptions = new BackgroundJobServerOptions() { ShutdownTimeout = TimeSpan.FromSeconds(5) };

            using (var server = new BackgroundJobServer(serverOptions, storage))
            {
                Console.WriteLine("Hangfire Server started. Press any key to exit...", LogLevel.Fatal);

                JobStorage.Current = storage;

                RecurringJob.AddOrUpdate(
                    () => action(),
                    Cron.Minutely);
                System.Console.ReadKey();
                Console.WriteLine("Stopping server...", LogLevel.Fatal);
            }

            Console.ReadLine();

        }

        public static void action()
        {
            IRepositoryConsole db = new RepositoryConsole("RedisMongoDB");

            var cache = RedisConnectorHelper.Connection.GetDatabase();

            var products = db.GetRecords<Product>();
            Console.WriteLine("Hangfire Service running !!!");
            foreach (var product in products)
            {
                if (product.counter > 3 && cache.KeyExists(product.PrimaryId) ==false)
                {
                    Product redisProduct = product;
                    redisProduct.Source = "Redis";
                    cache.StringSet(product.PrimaryId, JsonConvert.SerializeObject(redisProduct));
                    Console.WriteLine("Product added to Redis");
                }
            }
        }
    }
}
