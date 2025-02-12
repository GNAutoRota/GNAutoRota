using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace GNAutoRota.Classes
{
    public static class AppSettings
    {
        public static IConfiguration Configuration { get; private set; }

        static AppSettings()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "GNAutoRota.appsettings.json"; 

            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null)
                {
                    throw new FileNotFoundException($"Recurso '{resourceName}' não encontrado.");
                }

                var builder = new ConfigurationBuilder()
                    .AddJsonStream(stream);

                Configuration = builder.Build();
            }

        }

        public static string GetApiKey(string key)
        {
            return Configuration[$"ApiKeys:{key}"];
        }

    }
}
