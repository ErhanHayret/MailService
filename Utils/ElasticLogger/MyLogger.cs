using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using System;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Serilog.Formatting.Elasticsearch;

namespace Utils.ElasticLogger
{
    public static class MyLogger
    {
        public static ILogger Logger;
        static MyLogger()
        {
            ConfigureLogging();
        }
        public static void ConfigureLogging()
        {
            /*var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");//Development
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsetting.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true).Build();
            Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .WriteTo.Debug()
                .WriteTo.Console()
                .WriteTo.Elasticsearch(ConfigureElasticSink( environment))
                .Enrich.WithProperty("Environment", environment)
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
            Logger.Information("asdfgh");*/
            //Console.WriteLine(Environment.GetEnvironmentVariable("ELASTIC_ENVIRONMENT"));
            Logger = new LoggerConfiguration()
                  .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(Environment.GetEnvironmentVariable("ELASTIC_ENVIRONMENT")))
                  {
                      FailureCallback = e => Console.WriteLine("Unable to submit event " + e.MessageTemplate),
                      EmitEventFailure = EmitEventFailureHandling.WriteToSelfLog |
                                       EmitEventFailureHandling.RaiseCallback,
                      AutoRegisterTemplate = true,
                      AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv6,
                      IndexFormat = "mail-project-{0:yyyy.MM}",
                      MinimumLogEventLevel = Serilog.Events.LogEventLevel.Verbose,
                      CustomFormatter = new ExceptionAsObjectJsonFormatter()
                  })
                  .WriteTo.Console()
                  .CreateLogger();
        }

        private static ElasticsearchSinkOptions ConfigureElasticSink( string environment)
        {
            Console.WriteLine(new Uri(Environment.GetEnvironmentVariable("ELASTIC_ENVIRONMENT")).ToString());

            return new ElasticsearchSinkOptions(new Uri(Environment.GetEnvironmentVariable("ELASTIC_ENVIRONMENT")))
            {
                AutoRegisterTemplate = true,
                IndexFormat = $"{Assembly.GetExecutingAssembly().GetName().Name.ToLower().Replace(".", "-")}-{environment?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}"
            };
        }
    }
}
