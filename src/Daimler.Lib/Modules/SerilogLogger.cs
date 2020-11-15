using System;
using System.Collections.Generic;
using System.IO;
using Autofac;
using Autofac.Core;
using Daimler.Lib.Logger;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;
using Serilog.Formatting.Elasticsearch;
using Serilog.Sinks.Elasticsearch;

namespace Daimler.Lib.Modules
{
    public class SerilogLogger : IDaimlerLogger
    {
        private readonly ILogger _logger;

        public SerilogLogger()
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.With<DefaultContextEnricher>()
#if DEBUG
                .MinimumLevel.Override("Microsoft", LogEventLevel.Verbose)
                .MinimumLevel.Override("System", LogEventLevel.Verbose)
#else 
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .MinimumLevel.Override("System", LogEventLevel.Information)
#endif
                .WriteTo.RollingFile(Path.Combine("Logs", "log-{Date}.txt"), retainedFileCountLimit: 3)
                .WriteTo.Console(new RenderedCompactJsonFormatter())
                .MinimumLevel.Verbose()
                .CreateLogger();

            _logger = Log.Logger;
        }

        public SerilogLogger(string indexFormat, Uri elasticsearchUri)
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.With<DefaultContextEnricher>()
#if DEBUG
                .MinimumLevel.Override("Microsoft", LogEventLevel.Verbose)
                .MinimumLevel.Override("System", LogEventLevel.Verbose)
#else 
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .MinimumLevel.Override("System", LogEventLevel.Information)
#endif
                .WriteTo.RollingFile(Path.Combine("Logs", "log-{Date}.txt"), retainedFileCountLimit: 3)
                .WriteTo.Console(new RenderedCompactJsonFormatter())
                .WriteTo.Elasticsearch(
                    new ElasticsearchSinkOptions(
                        elasticsearchUri)
                    {
                        CustomFormatter = new ExceptionAsObjectJsonFormatter(renderMessage: true),
                        AutoRegisterTemplate = true,
                        IndexFormat = indexFormat,
                        TemplateName = "serilog-events-template"
                    })
                .MinimumLevel.Verbose()
                .CreateLogger();

            _logger = Log.Logger;
        }

        public void Information(string text)
        {
            _logger.Information(text);
        }

        public void Error(string text)
        {
            _logger.Error(text);
        }

        public void Error(Exception ex, string text)
        {
            _logger.Error(ex, text);
        }

        public void Warning(string text)
        {
            _logger.Warning(text);
        }

        public void Warning(Exception ex, string text)
        {
            _logger.Warning(ex, text);
        }
    }

    public class SerilogModule : Module
    {
        private readonly string _indexFormat;
        private readonly string _elasticsearchUri;


        public SerilogModule(IConfiguration configuration)
        {
            _indexFormat = configuration["ElasticsearchIndexFormat"];
            _elasticsearchUri = configuration["ElasticsearchUrl"];
        }

        protected override void Load(ContainerBuilder builder)
        {

            var parameters = new List<Parameter>();
            if (!string.IsNullOrWhiteSpace(_elasticsearchUri) && !string.IsNullOrWhiteSpace(_indexFormat))
            {
                parameters.Add(new NamedParameter("indexFormat", _indexFormat + "-{0:yyyy.MM.dd}"));
                parameters.Add(new NamedParameter("elasticsearchUri", new Uri(_elasticsearchUri)));
            };

            builder.RegisterType<SerilogLogger>().As<IDaimlerLogger>().SingleInstance().WithParameters(parameters);
        }
    }
}