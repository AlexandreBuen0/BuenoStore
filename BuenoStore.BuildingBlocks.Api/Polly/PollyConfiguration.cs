using Polly;
using Polly.Extensions.Http;
using Polly.Retry;

namespace BuenoStore.BuildingBlocks.Api.Polly
{
    public static class PollyExtensions
    {
        public static AsyncRetryPolicy<HttpResponseMessage> PoliticaDeRetentativa()
        {
            var retry = HttpPolicyExtensions
                .HandleTransientHttpError()
                .WaitAndRetryAsync(new[]
                {
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(5),
                    TimeSpan.FromSeconds(10),
                });

            return retry;
        }
    }
}
