using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

class Client2
{
    static async Task Main(string[] args)
    {
        string serverUri = "http://localhost:8080/";
        HttpClient httpClient = new HttpClient();
        string senderId = "Client2";
        string recipientId = "Client1";

        // Send a registration message to the server
        await SendMessage(httpClient, serverUri, senderId, recipientId, "REGISTER");

        // Listen for messages from Client 1
        while (true)
        {
            Console.WriteLine("Waiting for messages from Client 1...");
            string message = await ReceiveMessage(httpClient, serverUri, senderId);
            Console.WriteLine($"Received message from Client 1: {message}");

            // Check if it's the last message
            if (message == "LAST_MESSAGE")
            {
                Console.WriteLine("Received the last message from Client 1.");
                break;
            }
        }

        httpClient.Dispose();
    }

    static async Task SendMessage(HttpClient httpClient, string serverUri, string senderId, string recipientId, string message)
    {
        try
        {
            var messageContent = new StringContent(message, Encoding.UTF8, "text/plain");
            messageContent.Headers.Add("SenderId", senderId); // Include sender ID in message request
            messageContent.Headers.Add("RecipientId", recipientId); // Include recipient ID in message request
            await httpClient.PostAsync(serverUri, messageContent);
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Error sending message: {ex.Message}");
        }
    }
    static async Task<string> ReceiveMessage(HttpClient httpClient, string serverUri, string recipientId)
    {
        try
        {
            HttpResponseMessage response = await httpClient.GetAsync(serverUri);
            response.EnsureSuccessStatusCode();

            // Check if the response contains the recipient ID header
            if (response.Headers.TryGetValues("RecipientId", out IEnumerable<string> recipientIds))
            {
                foreach (var id in recipientIds)
                {
                    if (id == recipientId)
                    {
                        // Read the content of the response as string
                        return await response.Content.ReadAsStringAsync();
                    }
                }
            }

            Console.WriteLine("Recipient ID not found in the response headers.");
            return null;
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Error receiving message: {ex.Message}");
            return null;
        }
    }

}
