using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RA.Core.Composition;
using RA.Services.Hosting;

namespace quickybakkers.service.hosting
{
    public class Program
    {
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
            //obj.WithLogging(ConfigureLogging);
            obj.WithKestrel(ConfigureKestrelSettings);
        }

        private static void ConfigureKestrelSettings(KestrelServerOptions obj)
        {
            // set special kestrel settings here if required
        }

        /*private static void ConfigureLogging(WebHostBuilderContext context, ILoggingBuilder logging)
        {
            // Configure Serilog
            var config = new LoggerConfiguration();
            config.Enrich.FromLogContext();

            // Configure Serilog.Enrichers.Process
            config.Enrich.WithProcessId();
            config.Enrich.WithProcessName();

            // Configure Serilog.Enrichers.Thread
            config.Enrich.WithThreadId();

            // Configure Serilog.Enrichers.Context
            config.Enrich.WithMachineName();
            config.Enrich.WithUserName();


            // Log to SEQ via Serilog.Sinks.Seq
            //config.WriteTo.Seq("http://industry-dev-app.veibe.net:5341");


            // Create the global logger
            var logger = config.CreateLogger();

            // set static SeriLog logger
            Log.Logger = logger;

            // Configure ASP.NET to use SeriLog via Serilog.Logging.Extensions
            logging.AddSerilog(logger);
        }*/
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
