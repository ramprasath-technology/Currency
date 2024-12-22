using Polly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Polly.Retry;
using Polly.Timeout;
using Microsoft.Extensions.Logging;

namespace NetworkCalls.HTTP
{
    public class GetCalls : IGetCalls
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public GetCalls(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<T?> Get<T>(string url)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var resilienceBuilder = new ResiliencePipelineBuilder()
                    .AddRetry(new RetryStrategyOptions()
                    {
                        BackoffType = DelayBackoffType.Exponential,
                        Delay = TimeSpan.FromSeconds(1),
                        MaxRetryAttempts = 2,
                        UseJitter = true
                    })
                    .AddTimeout(new TimeoutStrategyOptions()
                    {
                        Timeout = TimeSpan.FromSeconds(7),
                    })
                    .Build();

                var response = await resilienceBuilder.ExecuteAsync(async (ct) => await client.GetAsync(url));

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var data = await response.Content.ReadFromJsonAsync<T>();
                    return data;

                }

                return default(T);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
    }
}
