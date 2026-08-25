## gRPC

- gRPC is a language agnostic, high performance Remote procedure call
- Benefits : 
		- Modern, high performance, lightweight RPC framework <br/>
		- Contract-First Api development, that uses Protocol Buffer by default, allowing language agnostic implementation <br/>
		- Support client-server, bi-directional streaming calls <br/>
		- Reduce network usage with Protobuf binary serialization. <br/>

### GRPC Types of method
	- Unary
	- Server Streaming
	- Client Streaming
	- Bi-Directional

<pre>
	syntax = "proto3";

	service ExampleService {
	  // Unary
	  rpc UnaryCall (ExampleRequest) returns (ExampleResponse);

	  // Server streaming
	  rpc StreamingFromServer (ExampleRequest) returns (stream ExampleResponse);

	  // Client streaming
	  rpc StreamingFromClient (stream ExampleRequest) returns (ExampleResponse);

	  // Bi-directional streaming
	  rpc StreamingBothWays (stream ExampleRequest) returns (stream ExampleResponse);
	}
</pre>

### Unary
A Unary method has the request as a parameter, and ereturn the response. A Unary call is completed when the response is returned.
<pre>
	public override Task &lt;ExampleResponse&gt; UnaryCall(ExampleRequest request,
	    ServerCallContext context)
	{
	    var response = new ExampleResponse();
	    return Task.FromResult(response);
	}
</pre> 

### Server Streaming
A Server streaming method has the request message as a paramter, Because multiple messages can be streamed back to the called, **response.WriteAsync** is used to send response message. A server streaming call is complete when the method returns.

<pre>
	public override async Task StreamingFromServer(ExampleRequest request,
	    IServerStreamWriter&lt;ExampleResponse&gt; responseStream, ServerCallContext context)
	{
	    for (var i = 0; i < 5; i++)
	    {
	        await responseStream.WriteAsync(new ExampleResponse());
	        await Task.Delay(TimeSpan.FromSeconds(1));
	    }
	}
</pre>

The client has no way to send additional messages or data once the server streaming method has started. Some streaming methods are designed to run forever. For continuous streaming methods, a client can cancel the call when it's no longer needed. When cancellation happens the client sends a signal to the server and the  ServerCallContext.CancellationToken is raised. The  `CancellationToken`  token should be used on the server with async methods so that:

-   Any asynchronous work is canceled together with the streaming call.
-   The method exits quickly.

<pre>
	public override async Task StreamingFromServer(ExampleRequest request,
	    IServerStreamWriter&lt;ExampleResponse&gt; responseStream, ServerCallContext context)
	{
	    while (!context.CancellationToken.IsCancellationRequested)
	    {
	        await responseStream.WriteAsync(new ExampleResponse());
	        await Task.Delay(TimeSpan.FromSeconds(1), context.CancellationToken);
	    }
	}
</pre>
