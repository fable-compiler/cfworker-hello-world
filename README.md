# FSharp/Fable 'Hello World' on Cloudflare Workers

This Repo shows how to run 'Hello World' in [FSharp](https://docs.microsoft.com/en-us/dotnet/fsharp/get-started/install-fsharp) on Cloudflare Workers via the [Fable](https://fable.io) JavaScript transpiler. Workers are a simple inexpensive way to execute functions on Cloudflare edge network. They can be used for anything from utilities to full-on WebAPI's. For a more detailed description of Workers in FSharp see:
* [Description of a FSharp 'Hello World' Worker](https://github.com/jbeeko/cfworker-hello-world)
* [Description of a FSharp WebAPI Worker](https://github.com/jbeeko/cfworker-web-api)

## Setting Up Your Environment

### Prerequisits
* A Cloudflare account, either [paid or free](https://dash.cloudflare.com/sign-up/workers). Needed to provide the hosting environment to which your worker will be deployed.
* [Wrangler](https://github.com/cloudflare/wrangler), the Cloudflare Workers CLI. This works with the webpack.config.js file to build and deploy your worker.
* [.NET SDK](https://dotnet.microsoft.com), used to generate an F# abstract syntax tree from which the JavaScript is generated.
* [Node.js](https://nodejs.org/en/), used to support the tooling to convert the AST to JavaScript.
* An editor with F# support. [VisualStudio Code with Ionide is recomended](https://docs.microsoft.com/en-us/dotnet/fsharp/get-started/install-fsharp#install-f-with-visual-studio-code).

### Install and Check Prerequisits
Perform the following as some simple checks to ensure the pre-requisits are in place. At time of writing the following were working:
* [Check](https://docs.microsoft.com/en-us/dotnet/fsharp/get-started/get-started-vscode) you are able to edit F# files.
* [Log into Cloudflare](https://dash.cloudflare.com/login), you should be able to view the workers pannel.
* `wrangler --version` -> v1.10.3
* `dotnet --version` -> .NET Core 3.1 or .Net 5.0
* `node -v` -> v12.18

### Configure Wrangler
To authenticate wrangler commands it is recomended you [configure wrangler](https://dash.cloudflare.com/sign-up/workers) with your APIKey using `wrangler config`.

## Generating and Testing a Worker

### Generate a New Project
To create a new project based on this template execute:
```
wrangler generate projectname https://github.com/fable-compiler/fable-cfworker
```

### Build and Deploy to Dev
To build and deploy the new worker execute `wrangler dev` from repo root. This builds and pushes the code to a cloud environment and starts a stub running locally for testing. Cloudflare has a blog [explaining](https://blog.cloudflare.com/announcing-wrangler-dev-the-edge-on-localhost/) how this works.

### Test the Dev Worker:
```
MBPro:~ $ curl localhost:8787
Hello from Fable at: Mon Oct 19 2020 19:30:39 GMT+0000 (Coordinated Universal Time)
```

### File Watcher
Wrangler Dev includes a file watcher. Make a small change to the response string in  `./src/Worker.fs` and save the file. Notice how the worker is automatically redeployed and a new invocation of cURL will return the modifed response.

## Publish to Your Cloudflare Account
To publish your worker to your Cloudflare account first configure a [route and zone id](https://developers.cloudflare.com/workers/cli-wrangler/configuration) in your `./wrangler.toml` file. Then execute `wrangler publish` this will deploy the worker as specified in the TOML file.

> **Note to Fablers:** Unlike most Fable projects Yarn/FAKE is not used but rather Wrangler drives the build and deploy steps. This works because the line `webpack_config = "webpack.config.js"` in the `wrangler.toml` file is able to kick-off the Fable toolchain putting the JavaScript artificts into the output directory specifed in `webpack.config.js` from which Wrangler deploys them.
>
> To keep things as simple as possible the project manages packages via the `.fsproj` file rather than with Paket.
