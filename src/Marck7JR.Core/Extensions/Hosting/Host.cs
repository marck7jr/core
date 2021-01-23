using Microsoft.Extensions.Hosting;

namespace Marck7JR.Core.Extensions.Hosting
{
    public static class Host
    {
        private readonly static IHostBuilder builder = Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder();
        private static IHost? host;

        public static IHostBuilder GetHostBuilder() => builder;
        public static IHost GetHost()
        {
            if (host is null)
            {
                host = builder.Build();
            }

            return host;
        }
    }
}
