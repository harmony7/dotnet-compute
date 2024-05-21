## .NET SDK for Fastly Compute

by Katsuyuki Omuro

This is an SDK and runtime library for building [.NET Core](https://dotnet.microsoft.com/en-us/)
applications for [Fastly Compute](https://www.fastly.com/documentation/guides/compute/).

This SDK works with [.NET 8](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-8/overview),
using the `wasi-experimental` workload pack (described [here](https://devblogs.microsoft.com/dotnet/extending-web-assembly-to-the-cloud/)).

> [!WARNING]
> Use at your own risk. This is a personal project that I'm working on in
> my free time. It's not owned or sponsored by Fastly, so please don't try
> to contact them for support with it. If you need help, try reaching out
> in the [issue tracker](https://github.com/harmony7/dotnet-compute/issues).

* [Documentation](./docs/index.md)
* For quick usage instructions, see the following: 
  * [Prerequisites](./src/HarmonicBytes.FastlyCompute/README.md#prerequisites) 
  * [Usage](./src/HarmonicBytes.FastlyCompute/README.md#usage) 
  * [Limitations](./src/HarmonicBytes.FastlyCompute/README.md#limitations) 
* [Package README](./src/HarmonicBytes.FastlyCompute/README.md)

### Directory Structure

* [src/](./src) - Class library sources
* [docs/](./docs) - Documentation
* [examples/](./examples) - Examples

### LICENSE

[Apache 2.0](./LICENSE)

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
