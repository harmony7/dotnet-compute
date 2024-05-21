## Basic Example in F#

This example shows a very basic F# program that uses the .NET library `HarmonicBytes.FastlyCompute` to run
some edge logic on every invocation, including getting information about the client request,
making an outgoing request to a backend, and modifying the response with some additional
headers before sending it back to the client.

### Running the example

For prerequisites, see the [Package README](../../src/HarmonicBytes.FastlyCompute/README.md#prerequisites).

You can run and experiment with this example locally using Fastly's
[local testing server](https://www.fastly.com/documentation/guides/compute/testing/#running-a-local-testing-server).
You can also [create a free Fastly developer account](https://www.fastly.com/signup/) to deploy this example to your
own Fastly service, and run it on Fastly's global edge network.

#### Running locally

```shell
cd basic-example-fsharp
fastly compute serve --verbose
```

#### Publish to Fastly

After configuring Fastly CLI, use the following commands:

```shell
cd basic-example-fsharp
fastly compute publish --verbose
```

### Concepts

This example illustrates the following ideas:

* Logging

  * Writing arbitrary string output to the Log, which can be seen using
    `fastly log-tail` (or the Viceroy output for local development).
      ```fsharp
      printfn "I'm alive in F#!"
      ```

* Getting information about the incoming request

  * Obtaining the `HttpRequestMessage` object that represents the
    HTTP request that invoked the execution. This object is standard in
    `System.Net.Http`.
      ```fsharp
      let httpRequestMessage = Invocation.GetRequest()
      ```

  * Obtaining information about the request by accessing its fields.
      ```fsharp
      printfn $"%s{httpRequestMessage.Method.ToString()}"
      match httpRequestMessage.Content with
      | null -> ()
      | content ->
          printfn $"Content-Type: %s{content.Headers.ContentType.ToString()}"
          let stream = content.ReadAsStream()
          printfn $"%s{(new StreamReader(stream)).ReadToEnd()}"
      ```

* Making a backend request
  
  * Instantiating a `FastlyHttpClient`, which shares a common parent class with
    `System.Net.Http.HttpClient`. It can be used to make requests to a
    [backend](https://www.fastly.com/documentation/reference/api/services/backend/).
      ```fsharp
      let httpClient = new FastlyHttpClient()
      ```

  * To prepare for making a request to a backend, instantiating a new instance of
    `HttpRequestMessage`, configuring it with a method, Uri, and a request header.
      ```fsharp
      let beReq = new HttpRequestMessage(HttpMethod.Get, "https://httpbin.org/anything")
      beReq.Headers.Add("Accept", "application/json")
      ```

  * Extending `HttpRequestMessage` with `FastlyOptions`,
    * applying a `Backend` property to indicate the name of the backend to send this request.
        ```fsharp
        beReq.GetFastlyOptions().Backend <- "httpbin"
        ```

    * applying a `CacheOverride` property to indicate a custom TTL on the cached response that would
      result from the request.   
        ```fsharp
        let cacheOverride = 
            CacheOverride("override", 
                          CacheOverrideInit(TimeToLive = uint 30))
        beReq.GetFastlyOptions().CacheOverride <- cacheOverride
        ```

    * applying a `CacheKey` property to indicate a custom caching key for the outgoing backend request 
        ```fsharp
        beReq.GetFastlyOptions().CacheKey <- "foo"
        ```

  * Making an HTTP Request to the backend. When the request completes, the response
    is returned as an instance of the `HttpResponseMessage` class, which is standard in
    `System.Net.Http`. (At the current moment, `Send` must be called with
    `CancellationToken.None`.)
      ```fsharp
      let httpResponseMessage = httpClient.Send(beReq, CancellationToken.None)
      ```

* Obtaining various data. In this demo, we add their values as headers on the response. 

  * Reading a value from [Compute environment variables](https://www.fastly.com/documentation/reference/compute/ecp-env/)
    of the form `FASTLY_*`.
      ```fsharp
      httpResponseMessage.Headers.Add("Fastly-Trace-Id", Environment.GetEnvironmentVariable("FASTLY_TRACE_ID"))
      ```

  * Reading information about the client connection, including the IP address and
    [Geolocation data](https://www.fastly.com/documentation/guides/concepts/geolocation/).
      ```fsharp
      let reqClientInfo = Invocation.GetClientInfo()
      httpResponseMessage.Headers.Add("Fastly-IP-Address", reqClientInfo.Address)
      httpResponseMessage.Headers.Add("Fastly-GeoInfo", reqClientInfo.GeoInfo.ToString())
      ```

  * Reading a value from the [Config store](https://www.fastly.com/documentation/guides/concepts/edge-state/dynamic-config/#config-stores).
      ```fsharp
      let testConfigStore = new ConfigStore("example_config_store")
      httpResponseMessage.Headers.Add("Fastly-Foo-Value", testConfigStore.["foo"])
      ```
  
  * Reading a value from the [Secret store](https://www.fastly.com/documentation/guides/concepts/edge-state/dynamic-config/#secret-stores).
      ```fsharp
      let exampleSecretStore = new SecretStore("example_secret_store")
      httpResponseMessage.Headers.Add("Fastly-Secret-Value", exampleSecretStore.["api-key"].Plaintext)
      ```
  
  * Reading a value from the [KV store](https://www.fastly.com/documentation/guides/concepts/edge-state/data-stores/#kv-stores).
      ```fsharp
      let exampleKvStore = new KVStore("example_kv_store")
      httpResponseMessage.Headers.Add("Fastly-KV-Value", (new StreamReader(exampleKvStore.["data"])).ReadToEnd())
      ```

  * Reading a value from an embedded resource that has been added to the assembly using a `<Project>` tag in the `.csproj` file.
      ```fsharp
      let exampleFromResource = Assembly.GetEntryAssembly().GetManifestResourceStream("App.resource-data.txt")
      httpResponseMessage.Headers.Add("Resource-Value", if exampleFromResource <> null then (new StreamReader(exampleFromResource)).ReadToEnd() else "(not found)")
      ``` 

* Emitting a response

  * Sending the response message back from the invocation. This response is identical to the
    response obtained from the backend request, with additional headers added in the previous steps.  
      ```fsharp
      Invocation.SendResponse(httpResponseMessage)
      ```
