using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml;
using Loggly;
using Loggly.Config;
using LogglySolutions.Api.Settings;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RA.Core.Composition;
using RA.Services.Hosting;
using Serilog;

namespace quickybakkers.service.hosting
{
    public class Program
    {
        private static string _environmentName;

        //public static void Main(string[] args)
        //{
        //    Bootstrapper.QuickStart<CustomStartup>(args);
        //}

        public static void Main(string[] args)
        {
            Bootstrapper.QuickStart<CustomStartup>(args, Configure);
        }

        private static void Configure(IRadiusBuilder obj)
        {
            obj.WithLogging(ConfigureLogging);
            obj.WithKestrel(ConfigureKestrelSettings);
        }

        //private static void SetupLogglyConfiguration(LogglySettings logglySettings)
        //{
        //    //Configure Loggly
        //    var config = LogglyConfig.Instance;
        //    config.CustomerToken = logglySettings.CustomerToken;
        //    config.ApplicationName = logglySettings.ApplicationName;
        //    config.Transport = new TransportConfiguration()
        //    {
        //        EndpointHostname = logglySettings.EndpointHostname,
        //        EndpointPort = logglySettings.EndpointPort,
        //        LogTransport = logglySettings.LogTransport
        //    };
        //    config.ThrowExceptions = logglySettings.ThrowExceptions;

        //    //Define Tags sent to Loggly
        //    config.TagConfig.Tags.AddRange(new ITag[]{
        //        new ApplicationNameTag {Formatter = "Application-{0}"},
        //        new HostnameTag { Formatter = "Host-{0}" }
        //    });
        //}

        private static Serilog.Sinks.Loggly.LogglyConfiguration GetLogglyConfiguration()
        {
            var config = new Serilog.Sinks.Loggly.LogglyConfiguration();
            config.ApplicationName = "Quickybakkers";
            config.CustomerToken = "e1a2ca27-3883-42d7-b1f8-c3c848acbada";
            config.Tags = new List<string>();
            config.IsEnabled = true;
            config.ThrowExceptions = true;
            config.LogTransport = Serilog.Sinks.Loggly.TransportProtocol.Https;
            config.EndpointHostName = "logs-01.loggly.com";
            config.EndpointPort = 443;
            //bool OmitTimestamp = "";
            
            return config;
        }

        private static void ConfigureKestrelSettings(KestrelServerOptions obj)
        {
            // set special kestrel settings here if required
        }

        private static void ConfigureLogging(WebHostBuilderContext context, ILoggingBuilder logging)
        {
            //var configuration = new ConfigurationBuilder()
            //    .SetBasePath(Directory.GetCurrentDirectory())
            //            .AddJsonFile("appsettings.json")
            //            .AddJsonFile($"appsettings.{_environmentName}.json", optional: true, reloadOnChange: true)
            //            .Build();
            //
            //var logglySettings = new LogglySettings();
            //configuration.GetSection("Serilog:Loggly").Bind(logglySettings);
            //
            //SetupLogglyConfiguration(logglySettings);

            //Log.Logger = new LoggerConfiguration()
            //    .ReadFrom.Configuration(configuration)
            //    .CreateLogger();

            //Log.Information("Loggly settings loaded");

            // Configure Serilog
            var config = new LoggerConfiguration();
            config.Enrich.FromLogContext();

            // Configure Serilog.Enrichers.Process
            //config.Enrich.WithProcessId();
            //config.Enrich.WithProcessName();

            // Configure Serilog.Enrichers.Thread
            //config.Enrich.WithThreadId();

            // Configure Serilog.Enrichers.Context
            //config.Enrich.WithMachineName();
            //config.Enrich.WithUserName();

            //Loggly config
            var logglyConfiguration = GetLogglyConfiguration();

            // Log to SEQ via Serilog.Sinks.Seq
            //config.WriteTo.Seq("http://industry-dev-app.veibe.net:5341");
            config.WriteTo.Console(Serilog.Events.LogEventLevel.Verbose);
            if(Directory.Exists("C:\\Temp"))
                config.WriteTo.RollingFile("C:\\Temp\\LogglySolutions-API-{Date}.txt", Serilog.Events.LogEventLevel.Verbose, "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message}{NewLine}{Exception}");
            config.WriteTo.Loggly(Serilog.Events.LogEventLevel.Verbose, 10, null, null, null, null, 1048576, null, null, null, logglyConfiguration, null);

            // Create the global logger
            var logger = config.CreateLogger();

            // set static SeriLog logger
            Log.Logger = logger;

            // Configure ASP.NET to use SeriLog via Serilog.Logging.Extensions
            logging.AddSerilog(logger);

            Log.Logger.Information("Logging configuration done.");
        }
    }

    [Export(typeof(IHostControl))]
    public class HostControl : ExecutableHostControl
    {
    }

    internal class CustomStartup : Startup
    {
        public CustomStartup(IHostingEnvironment env) : base(env)
        {
        }

        protected override IModuleLoader ModuleLoader => new CustomModuleLoader();
    }

    internal class CustomModuleLoader : BinModuleLoader
    {
        protected override IEnumerable<Assembly> FilterBinModuleAssemblies(IEnumerable<Assembly> assemblies)
        {
            assemblies = assemblies.Where(t => t.FullName.StartsWith("Quickybakkers.Service"));
            return assemblies;
        }

        protected override IEnumerable<string> FilterBinModuleAssemblies(IEnumerable<string> files) => files;
    }
}
