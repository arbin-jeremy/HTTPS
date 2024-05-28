using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

class HttpsServer
{
    static readonly Dictionary<string, HttpListenerContext> clientConnections = new Dictionary<string, HttpListenerContext>();

    static async Task Main(string[] args)
    {
        HttpListener listener = new HttpListener();
        listener.Prefixes.Add("http://localhost:8080/");
        listener.Start();
        Console.WriteLine("Server listening on http://localhost:8080/");

        try
        {
            while (true)
            {
                HttpListenerContext context = await listener.GetContextAsync();

                // Extract sender and recipient IDs from the request headers
                string senderId = context.Request.Headers["SenderId"];
                string recipientId = context.Request.Headers["RecipientId"];

                // Check if both sender and recipient IDs are provided
                if (!string.IsNullOrEmpty(senderId) && !string.IsNullOrEmpty(recipientId))
                {
                    Console.WriteLine($"Received message from client {senderId} for client {recipientId}");

                    // Store client connection with its ID
                    clientConnections[recipientId] = context;

                    // Forward the message to the recipient client
                    if (clientConnections.TryGetValue(recipientId, out HttpListenerContext recipientContext))
                    {
                        // Forward the message to the recipient client
                        using (StreamReader reader = new StreamReader(context.Request.InputStream, context.Request.ContentEncoding))
                        {
                            string requestBody = await reader.ReadToEndAsync();
                            byte[] responseBytes = Encoding.UTF8.GetBytes(requestBody);
                            recipientContext.Response.ContentLength64 = responseBytes.Length;
                            recipientContext.Response.ContentType = "text/plain";

                            // Add recipient ID header to the response
                            recipientContext.Response.Headers.Add("RecipientId", recipientId);

                            await recipientContext.Response.OutputStream.WriteAsync(responseBytes, 0, responseBytes.Length);
                            recipientContext.Response.Close();
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Recipient with ID '{recipientId}' not found.");
                    }
                }
                else
                {
                    Console.WriteLine("Sender ID or Recipient ID not found in request headers.");
                }
                context.Response.Close();
            }
        }
        finally
        {
            listener.Close();
        }
    }
}
