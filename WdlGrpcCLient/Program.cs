using Grpc.Net.Client;

namespace WdlGrpcCLient
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:7248");
            var client = new Greeter.GreeterClient(channel);

            var reply = await client.SayHelloAsync(new HelloRequest { Name = "World" });
            Console.WriteLine("Greeting: " + reply.Message);

            // Assuming you added a SayGoodbye method as well...
            var goodbyeReply = await client.SayGoodbyeAsync(new GoodbyeRequest { Name = "World" });
            Console.WriteLine("Goodbye: " + goodbyeReply.Message);


            Console.WriteLine("Hello, World!");
        }
    }
}