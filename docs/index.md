# .NET SDK for Fastly Compute

by Katsuyuki Omuro

This is an SDK and runtime library for building .NET Core applications for
[Fastly Compute](https://www.fastly.com/documentation/guides/compute/).

This SDK works with [.NET 8](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-8/overview),
using the `wasi-experimental` workload pack (described [here](https://devblogs.microsoft.com/dotnet/extending-web-assembly-to-the-cloud/)).

> [!WARNING]
> Use at your own risk. This is a personal project that I'm working on in
> my free time. It's not owned or sponsored by Fastly, so please don't try
> to contact them for support with it. If you need help, try reaching out
> in the [issue tracker](https://github.com/harmony7/dotnet-compute/issues).

> [!NOTE]
> Docs are a work in progress.

Class library documentation will be coming, it's likely I'll be creating them with
a tool such as [DocFX](https://dotnet.github.io/docfx/).

## What’s Fastly Compute?

[Fastly Compute](https://www.fastly.com/documentation/guides/compute/) is a platform that features a computing system that
runs user code on Fastly's global edge network. The modules it accepts are to be in WebAssembly, enabling users to provide
code in any language that can target WebAssembly and for which an SDK is available.

## HarmonicBytes.FastlyCompute

`HarmonicBytes.FastlyCompute` is an SDK library, provided as a Nuget package, that can be used to build a .NET Core application
that runs on Fastly Compute. Because the library targets .NET, it can be consumed by any .NET language, including C#,
F#, and Visual Basic.

By providing an alternative SDK that can be used to author Fastly Compute applications, this library gives users additional
options when writing code for the Fastly Compute platform.

## What is .NET Core? 

[.NET Core](https://learn.microsoft.com/en-us/dotnet/core/introduction) is a general purpose, free, cross-platform, and
open-source platform for building many kinds of applications. Programs can be written in multiple languages (C# being
one of the most popular), all of which compile to an intermediary bytecode, which finally runs on a runtime designed for the
target platform.

The platform provides many useful features to help with developer productivity, code performance, and code security, such
as garbage collection, type safety, and memory safety. There is also a large open-source community, enabling users to
tap into packages that are made available through [NuGet](https://nuget.org/), the package manager and repository for .NET,
as well as through other open-source venues such as [GitHub](https://github.org/).

## How does it work?

Your application is built just as a normal application would be, i.e., into [IL](https://learn.microsoft.com/en-us/dotnet/standard/glossary#il).
The build tools then bundle your application along with its dependencies into a single `.wasm` file along with `dotnet.wasm`,
the .NET runtime for WebAssembly. Your application runs on the Fastly Compute platform under [just-in-time (JIT) compilation](https://learn.microsoft.com/en-us/dotnet/standard/glossary#jit).

(Theoretically, [ahead-of-time (AOT) compilation](https://learn.microsoft.com/en-us/dotnet/standard/glossary#aot) should
be possible as well, and [support seems to be coming soon](https://github.com/dotnet/runtime/issues/82691).)

## How do I use it?

To use this SDK, your application is expected to be a "Console App" that targets the `wasi-wasm` [runtime identifier](https://learn.microsoft.com/en-us/dotnet/core/rid-catalog).
This target becomes available in .NET 8 after you install the `wasi-experimental` workload, described in
[this post on the .NET Blog](https://devblogs.microsoft.com/dotnet/extending-web-assembly-to-the-cloud/).

Your program should install the `HarmonicBytes.FastlyCompute` package from Nuget.
`HarmonicBytes.FastlyCompute` is written in C#, and provides interop with the low-level hostcalls provided by the Fastly
Compute platform. It exposes these as a class library to your .NET application.

Your application's code is called on every invocation of your service, i.e., every time a request arrives.
At minimum, your application should send an outgoing response by instantiating an instance of `HttpResponseMessage` and
passing it to `Invocation.SendResponse()`.

Once your application is ready to build, build it using the `dotnet publish` command, and then run it using
`fastly compute serve`, or publish it to your service using `fastly compute publish`.

The above is set up automatically when you use the starter kit, found at [harmony7/compute-starter-kit-csharp-basic](https://github.com/harmony7/compute-starter-kit-csharp-basic),
using the [Fastly CLI](https://www.fastly.com/documentation/reference/tools/cli/).

```shell
mkdir my-app
cd my-app
fastly compute init --from=https://github.com/harmony7/compute-starter-kit-csharp-basic
```

For details on installation and use, see the [HarmonicBytes.FastlyCompute package README](../src/HarmonicBytes.FastlyCompute/README.md).

## Limitations

For a current list of known limitations, see [Limitations in the package README](../src/HarmonicBytes.FastlyCompute/README.md#limitations).

## FAQ

When I get enough questions about something I'll start posting my responses here.

### License

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
