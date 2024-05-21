using System.Reflection;
using System.Web;
using HarmonicBytes.FastlyCompute;
using HarmonicBytes.FastlyCompute.Http;

Console.WriteLine(
    $"FASTLY_SERVICE_VERSION: {Environment.GetEnvironmentVariable("FASTLY_SERVICE_VERSION")}");

Console.WriteLine("I'm alive in C#!");

var httpRequestMessage = Invocation.GetRequest();

Console.WriteLine(httpRequestMessage.Method);
if (httpRequestMessage.Content != null)
{
    Console.WriteLine("Content-Type: " + httpRequestMessage.Content.Headers.ContentType);
    var stream = httpRequestMessage.Content.ReadAsStream();
    Console.WriteLine(new StreamReader(stream).ReadToEnd());
}

if (httpRequestMessage.RequestUri != null)
{
    var queryParams = HttpUtility.ParseQueryString(httpRequestMessage.RequestUri.Query);
    var purgeParam = queryParams["purge"];
    if (purgeParam != null)
    {
        try
        {
            var surrogateKeyReturn = Purge.PurgeSurrogateKey(purgeParam);
            Console.WriteLine("surrogateKeyReturn: " + surrogateKeyReturn);
        }
        catch (FastlyException ex)
        {
            if (ex.FastlyError != FastlyError.Unsupported)
            {
                throw;
            }
            Console.WriteLine("PurgeSurrogateKey not supported on this platform");
        }
    }
}

var httpClient = new FastlyHttpClient();

var beReq = new HttpRequestMessage(HttpMethod.Get, "https://httpbin.org/anything");
beReq.Headers.Add("Accept", "application/json");
beReq.GetFastlyOptions().Backend = "httpbin";

beReq.GetFastlyOptions().CacheOverride =
    new CacheOverride("override", new CacheOverrideInit { TimeToLive = 30, SurrogateKey = "foo1 foo2" });

beReq.GetFastlyOptions().CacheKey = "foo";

var httpResponseMessage = httpClient.Send(beReq, CancellationToken.None);

httpResponseMessage.Headers.Add("Fastly-Trace-Id", Environment.GetEnvironmentVariable("FASTLY_TRACE_ID"));

var reqClientInfo = Invocation.GetClientInfo();
httpResponseMessage.Headers.Add("Fastly-IP-Address", reqClientInfo.Address);
httpResponseMessage.Headers.Add("Fastly-GeoInfo", reqClientInfo.GeoInfo?.ToString());

var testConfigStore = new ConfigStore("example_config_store");
httpResponseMessage.Headers.Add("Fastly-Foo-Value", testConfigStore["foo"]);

var exampleSecretStore = new SecretStore("example_secret_store");
httpResponseMessage.Headers.Add("Fastly-Secret-Value", exampleSecretStore["api-key"].Plaintext);

var exampleKvStore = new KVStore("example_kv_store");
httpResponseMessage.Headers.Add("Fastly-KV-Value", new StreamReader(exampleKvStore["data"]).ReadToEnd());

var exampleFromResource = Assembly.GetEntryAssembly()?.GetManifestResourceStream("App.resource-data.txt");
httpResponseMessage.Headers.Add("Resource-Value", exampleFromResource != null ? new StreamReader(exampleFromResource).ReadToEnd() : "(not found)");

Invocation.SendResponse(httpResponseMessage);
