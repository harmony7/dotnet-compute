## .NET SDK for Fastly Compute

by Katsuyuki Omuro

This is an SDK and runtime library for building .NET Core applications for
[Fastly Compute](https://www.fastly.com/documentation/guides/compute/).

This SDK works with [.NET 8](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-8/overview),
using the `wasi-experimental` workload pack (described [here](https://devblogs.microsoft.com/dotnet/extending-web-assembly-to-the-cloud/)).

> [!WARNING]
> Use at your own risk. This is a personal project that I'm working on in
> my free time. It's not owned or sponsored by Fastly, so please don't try
> to contact them for support with it. If you need help, please visit the
> [GitHub project page](https://github.com/harmony7/dotnet-compute).

### Prerequisites

To work with this library, you will need the following tools:

* .NET 8 SDK
  * Obtain it at the [.NET 8 Downloads Page](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
  * The SDK package is all you need, it includes all the tooling as well as the runtime.

> [!WARNING]
> This will NOT currently work with .NET 9.

* WASI SDK
  * Obtain the release for your platform on the [wasi-sdk GitHub releases Page](https://github.com/WebAssembly/wasi-sdk/releases)
  * Extract it to some permanent location on your computer (I like `/opt/wasi-sdk`).
  * Add an environment variable `WASI_SDK_PATH` pointing to the extracted directory.

* WASI-Experimental workload for .NET 8
  * `dotnet workload install wasi-experimental`

> [!TIP]
> If you update .NET 8 SDK (even by a patch release, such as `8.0.204` to `8.0.300`) you will have to
> perform this step again, as workloads on your system are managed by SDK release.

* Fastly CLI
  * You'll need this to package and run your Compute application locally or to
    upload it to your Fastly service.
  * [Installation Instructions](https://www.fastly.com/documentation/reference/tools/cli/)

* (optional) binaryen - This is optional and if it exists the WASI SDK will use it to optimize your Wasm builds.
  * [Release Page](https://github.com/WebAssembly/binaryen/releases)
  * Extract it somewhere and add it to your `PATH`.
  * Alternatively, if you're using macOS with [Homebrew](https://brew.sh), you can use `brew install binaryen`.

The recommended language is C#, though any .NET language should work.

You'll also want to create a [Fastly Account](https://www.fastly.com/signup/) (the free developer account is sufficient)
to deploy and run your service at the edge.

### Usage

It's recommended that you create an application using the starter kit, found
at [harmony7/compute-starter-kit-csharp-basic](https://github.com/harmony7/compute-starter-kit-csharp-basic),
using the [Fastly CLI](https://www.fastly.com/documentation/reference/tools/cli/).

```shell
mkdir my-app
cd my-app
fastly compute init --from=https://github.com/harmony7/compute-starter-kit-csharp-basic
```

You'll be prompted for the project name, description, and author.

Afterward, your project will be initialized from the starter kit, and will
have the following structure:

```
my-app/
  ComputeApp/                            - Directory for Fastly Compute source files
    Properties/AssemblyInfo.cs           - AssemblyInfo manifest file  
    ComputeApp.csproj                    - C# Project file for Compute application
    Program.cs                           - Program entry file
  fastly.toml                            - The Fastly Compute manifest file
  README.md                              - README file of the starter kit
  compute-starter-kit-csharp-basic.sln   - Visual Studio Solution file
  global.json                            - .NET Core configuration file
```

To work with your program, you'll want to work within the `ComputeApp/` directory,
starting mainly with the `Program.cs` file. You're free to add files
and bring in other libraries using NuGet from within the `ComputeApp`.

To run your program, switch to your project's directory (the directory that contains
the `fastly.toml` file), and then type the following:

```shell
fastly compute serve --verbose
```

This will build and run the Compute application in Fastly's
[local testing server](https://www.fastly.com/documentation/guides/compute/testing/#running-a-local-testing-server),
which will be browsable by default at [http://localhost:7676/](http://localhost:7676/).

When you're ready to deploy your application to your Fastly service,
type the following:

```shell
fastly compute publish --verbose
```

### More details

For more details on how the library works, visit the docs on the
[GitHub project](https://github.com/harmony7/dotnet-compute/blob/main/docs/index.md).

### Features

The following features are implemented:

* Can send log output to `stdout`
* Obtain incoming request, represented as `HttpRequestMessage`
    * `Method`
    * `Uri`
    * `HttpVersion`
    * `Headers`
    * `Content` + `Content.Headers`
* Send final response to client, represented as `HttpResponseMessage`
    * `Status`
    * `Headers`
    * `Content` + `Content.Headers`
* Can create new `HttpRequestMessage` to represent backend requests
* Adding Fastly custom options to `HttpRequestMessage` as `FastlyHttpOptions`
    * `Backend` - to associate a backend request with a Fastly backend
    * `CacheOverride` - can set Cache Override on backend requests
    * `CacheKey` - can set a custom cache key on backend requests
* Implementation of `FastlyHttpClient` a custom `HttpMessageInvoker`
    * Uses `FastlyHttpMessageHandler`, a custom `HttpMessageHandler` implementation.
    * Can make an outgoing backend request based on `HttpRequestMessage`
        * Honors `FastlyHttpOptions`
    * Can receive response of backend request as `HttpResponseMessage`
* Can redirect current request to the GRIP Proxy
    * `Invocation.RedirectToGripProxy()`
* Can get client info from request
    * `Invocation.GetClientInfo()`
* Can get Geolocation
    * Current request - `Invocation.GetClientInfo().GeoInfo`
    * Arbitrary IP address - `Geo.GetGeolocationForIpAddress()`
* Can get TLS protocol properties
    * JA3 MD5 hash - `Invocation.GetClientInfo().TlsJa3Md5`
    * Cipher OpenSSL Name - `Invocation.GetClientInfo().TlsCipherOpensslName`
    * Protocol ID - `Invocation.GetClientInfo().TlsProtocol`
    * Client Certificate (for mTLS) - `Invocation.GetClientInfo().TlsClientCertificate`
    * Client Hello - `Invocation.GetClientInfo().TlsClientHello`
* Can get values from Config Store
* Can get values from Secret Store
* Can read values from / write values to KV Store
* Can purge surrogate keys

The following features are planned but not yet implemented:

* Can send output to Fastly Named Log Providers
* Can get properties of Backends
    * Exists
    * Is healthy
* Can set Framing Headers Mode on backend requests
* Can use Dynamic Backend on backend requests
    * Manual configuration
    * Automatic configuration
* Can enable "auto decompress GZIP responses" on backend requests
* Can work with the Simple Cache API
    * Lookup
    * Insert
    * Transactional Lookup
    * Transactional Insert
* Can work with the Edge Rate Limiter
* Can work with Device Detection
* Can upgrade current request as WebSocket pass-through

The following features are being investigated:

* [async/await (EAP)](https://learn.microsoft.com/en-us/dotnet/standard/asynchronous-programming-patterns/interop-with-other-asynchronous-patterns-and-types#tasks-and-the-event-based-asynchronous-pattern-eap) in some way
    * This would enable things like SendAsync / SendAsyncStreaming
* Allow streamed sending of backend request body.
* Allow use of streaming response mode in `SendResponse()`
* Framework to allow user to write their app alternatively as a `HttpMessageHandler`
    * Would also possibly allow the use of Wizer to speed up instantiation

### Limitations

> [!WARNING]
> Some of these limitations are platform limitations and others are
> simply features that have not yet been designed/implemented.

At the current moment, multithreading is not supported. Anything that attempts to
run something in a secondary thread, such as `Task.Run()`, is not supported.

This means that at the current time, making multiple simultaneous outgoing requests
is not possible.

It _is_ possible to receive the incoming request as a stream. When the invocation
of your code begins, the request's headers have been received but the body may still
be in transit. You can read from the `Content` as a stream, receiving each chunk
in a blocking call.

It _is not_ (currently) possible to send the body of a backend request as a stream.
The content you provide will be sent in its entirety when you call `FastlyHttpClient.Send()`.

It _is_ possible to receive the response from the backend as a stream. When `FastlyHttpClient.Send()`
returns, the response's status and headers will have been received but the body may
still  be in transit. You can read from the `Content` as a stream, receiving each
chunk in a blocking call.

It _is not_ (currently) possible to send the final response to the caller as a stream.
The response you provide will be sent in its entirety when you call `Invocation.SendResponse()`.

### LICENSE

[Apache 2.0](https://github.com/harmony7/dotnet-compute/LICENSE)

Copyright 2024 Katsuyuki Omuro

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
