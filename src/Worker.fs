module Worker

open Fable.Core
open Fable.Core.JS
open Fable.Core.JsInterop

open Fetch
open Fetch.Utils
open Fable.Cloudflare.Workers


//The worker code is here. Define a request handler which creates an an
// appropreate Response and returns a Promise<Response>
let private handleRequest (req:Request) =
    promise {
        // YOUR CODE HERE
        let txt = sprintf "Hello from Fable at: %A" System.DateTime.Now
        let status : ResponseInit = !! {| status = "200" |}
        let response = newResponse txt status
        return response }


// Register a listner for the ServiceWorker 'fetch' event. That listner
// will extract the request and dispath it to the request handler.
addEventListener_fetch (fun (e:FetchEvent) ->
    e.respondWith (!^ (handleRequest e.request)))
