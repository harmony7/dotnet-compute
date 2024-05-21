open HarmonicBytes.FastlyCompute
open HarmonicBytes.FastlyCompute.Http
open System
open System.IO
open System.Net.Http
open System.Reflection
open System.Threading
open System.Web

printfn $"""FASTLY_SERVICE_VERSION: {Environment.GetEnvironmentVariable("FASTLY_SERVICE_VERSION")}"""

printfn "I'm alive in F#!"

let httpRequestMessage = Invocation.GetRequest()

printfn $"%s{httpRequestMessage.Method.ToString()}"
match httpRequestMessage.Content with
| null -> ()
| content ->
    printfn $"Content-Type: %s{content.Headers.ContentType.ToString()}"
    let stream = content.ReadAsStream()
    printfn $"%s{(new StreamReader(stream)).ReadToEnd()}"

if httpRequestMessage.RequestUri <> null then
    let queryParams = HttpUtility.ParseQueryString(httpRequestMessage.RequestUri.Query)
    let purgeParam = queryParams.["purge"]
    if purgeParam <> null then
        try
            let surrogateKeyReturn = Purge.PurgeSurrogateKey(purgeParam)
            printfn $"surrogateKeyReturn: {surrogateKeyReturn}"
        with
        | :? FastlyException as ex when ex.FastlyError <> FastlyError.Unsupported ->
            raise ex
        | _ ->
            printfn "PurgeSurrogateKey not supported on this platform"

let httpClient = new FastlyHttpClient()

let beReq = new HttpRequestMessage(HttpMethod.Get, "https://httpbin.org/anything")
beReq.Headers.Add("Accept", "application/json")
beReq.GetFastlyOptions().Backend <- "httpbin"

let cacheOverride = 
    CacheOverride("override", 
                  CacheOverrideInit(TimeToLive = uint 30, SurrogateKey = "foo1 foo2"))
beReq.GetFastlyOptions().CacheOverride <- cacheOverride

beReq.GetFastlyOptions().CacheKey <- "foo";

let httpResponseMessage = httpClient.Send(beReq, CancellationToken.None)

httpResponseMessage.Headers.Add("Fastly-Trace-Id", Environment.GetEnvironmentVariable("FASTLY_TRACE_ID"))

let reqClientInfo = Invocation.GetClientInfo()
httpResponseMessage.Headers.Add("Fastly-IP-Address", reqClientInfo.Address)
httpResponseMessage.Headers.Add("Fastly-GeoInfo", reqClientInfo.GeoInfo.ToString())

let testConfigStore = new ConfigStore("example_config_store")
httpResponseMessage.Headers.Add("Fastly-Foo-Value", testConfigStore.["foo"])

let exampleSecretStore = new SecretStore("example_secret_store")
httpResponseMessage.Headers.Add("Fastly-Secret-Value", exampleSecretStore.["api-key"].Plaintext)

let exampleKvStore = new KVStore("example_kv_store")
httpResponseMessage.Headers.Add("Fastly-KV-Value", (new StreamReader(exampleKvStore.["data"])).ReadToEnd())

let exampleFromResource = Assembly.GetEntryAssembly().GetManifestResourceStream("App.resource-data.txt")
httpResponseMessage.Headers.Add("Resource-Value", if exampleFromResource <> null then (new StreamReader(exampleFromResource)).ReadToEnd() else "(not found)")

Invocation.SendResponse(httpResponseMessage)
