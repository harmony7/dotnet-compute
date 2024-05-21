Imports HarmonicBytes.FastlyCompute
Imports HarmonicBytes.FastlyCompute.Http
Imports System.IO
Imports System.Net.Http
Imports System.Reflection
Imports System.Threading
Imports System.Web

Module MainModule
    Sub Main()
        Console.WriteLine(
            $"FASTLY_SERVICE_VERSION: {Environment.GetEnvironmentVariable("FASTLY_SERVICE_VERSION")}")
        
        Console.WriteLine("I'm alive in Visual Basic!")

        Dim httpRequestMessage As HttpRequestMessage = Invocation.GetRequest()

        Console.WriteLine(httpRequestMessage.Method)
        If httpRequestMessage.Content IsNot Nothing Then
            Console.WriteLine("Content-Type: " & httpRequestMessage.Content.Headers.ContentType.ToString())
            Dim stream As Stream = httpRequestMessage.Content.ReadAsStream()
            Console.WriteLine(New StreamReader(stream).ReadToEnd())
        End If
        
        If httpRequestMessage.RequestUri IsNot Nothing Then
            Dim queryParams = HttpUtility.ParseQueryString(httpRequestMessage.RequestUri.Query)
            Dim purgeParam = queryParams("purge")
            If purgeParam IsNot Nothing Then
                Try
                    Dim surrogateKeyReturn = Purge.PurgeSurrogateKey(purgeParam)
                    Console.WriteLine("surrogateKeyReturn: " & surrogateKeyReturn)
                Catch ex As FastlyException
                    If ex.FastlyError <> FastlyError.Unsupported Then
                        Throw
                    End If
                    Console.WriteLine("PurgeSurrogateKey not supported on this platform")
                End Try
            End If
        End If

        Dim httpClient As New FastlyHttpClient()

        Dim beReq As New HttpRequestMessage(HttpMethod.Get, "https://httpbin.org/anything")
        beReq.Headers.Add("Accept", "application/json")
        beReq.GetFastlyOptions().Backend = "httpbin"

        beReq.GetFastlyOptions().CacheOverride = _
            New CacheOverride("override", New CacheOverrideInit With {.TimeToLive = 30, .SurrogateKey = "foo1 foo2"})
        
        beReq.GetFastlyOptions().CacheKey = "foo"

        Dim httpResponseMessage As HttpResponseMessage = httpClient.Send(beReq, CancellationToken.None)

        httpResponseMessage.Headers.Add("Fastly-Trace-Id", Environment.GetEnvironmentVariable("FASTLY_TRACE_ID"))

        Dim reqClientInfo As ClientInfo = Invocation.GetClientInfo()
        httpResponseMessage.Headers.Add("Fastly-IP-Address", reqClientInfo.Address)
        If reqClientInfo.GeoInfo IsNot Nothing Then
            httpResponseMessage.Headers.Add("Fastly-GeoInfo", reqClientInfo.GeoInfo.ToString())
        End If

        Dim testConfigStore As New ConfigStore("example_config_store")
        httpResponseMessage.Headers.Add("Fastly-Foo-Value", testConfigStore("foo"))

        Dim exampleSecretStore As New SecretStore("example_secret_store")
        httpResponseMessage.Headers.Add("Fastly-Secret-Value", exampleSecretStore("api-key").Plaintext)

        Dim exampleKvStore As New KVStore("example_kv_store")
        httpResponseMessage.Headers.Add("Fastly-KV-Value", New StreamReader(exampleKvStore("data")).ReadToEnd())

        Dim exampleFromResource As Stream = Assembly.GetEntryAssembly()?.GetManifestResourceStream("App.resource-data.txt")
        httpResponseMessage.Headers.Add("Resource-Value", If(exampleFromResource IsNot Nothing, New StreamReader(exampleFromResource).ReadToEnd(), "(not found)"))

        Invocation.SendResponse(httpResponseMessage)
    End Sub
End Module
